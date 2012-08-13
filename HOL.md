<a name="HOLTop" />
# Sending Windows 8 Push Notifications using Windows Azure and the Windows Push Notification Service (JS) #
---

<a name="Overview" />
## Overview ##
In this hands-on lab, you will learn how to deploy a version of the [Windows Azure Toolkit for Windows 8](http://watwindows8.codeplex.com/) to Windows Azure and then utilize this deployment to send notifications to your client application via the [Windows Push Notification Service (WNS)] (http://msdn.microsoft.com/en-us/library/windows/apps/hh465460\(v=vs.85\).aspx). By the end of this lab you will have a fully functional portal capable of sending Toast, Tile and Badge notifications to your Windows 8 Style UI client application.

![Windows Azure Toolkit for Windows 8 delivering a notification via WNS](images/windows-azure-toolkit-for-windows-8-deliverin.png?raw=true)

_Windows Azure Toolkit for Windows 8 delivering a notification via WNS_

If you would like to learn more about how this lab works please see [this video](http://channel9.msdn.com/Events/TechDays/TechDays-2012-Belgium/272).

<a name="Objectives" />
### Objectives ###

In this hands-on lab, you will learn how to:

- Use the Windows Azure Management Portal to create storage accounts and hosted service components.
- Use the Windows Push Notification and Live Connect Portal to request credentials for use with WNS.
- Deploy web site using Web Deploy.
- Configure a Windows 8 Style UI client to receive notifications
- Test sending notifications to your client app via WNS using the Windows Azure Toolkit for Windows 8 portal.

<a name="Prerequisites" />
### Prerequisites ###

You must have the following items to complete this lab:

- [Visual Studio 2012 Express for Windows 8][1] or greater.
- A Windows Azure subscription with the Web Sites Preview enabled - [sign up for a free trial](http://aka.ms/WATK-FreeTrial)

[1]:http://msdn.microsoft.com/en-us/windows/apps/hh852659

>**Note:** This lab was designed to use Windows 8 Operating System.

<a name="Setup" />
### Setup ###
In order to execute the exercises in this hands-on lab you need to set up your environment.

1. Open a Windows Explorer window and browse to the lab’s **source** folder.

1. Execute the **Setup.cmd** file with Administrator privileges to launch the setup process that will configure your environment.

1. If the User Account Control dialog is shown, confirm the action to proceed.

>**Note:** Make sure you have checked all the dependencies for this lab before running the setup.

---

<a name="Exercises" />
## Exercises ##
This hands-on lab includes the following exercises:

*	[Getting Started: Deploying the Notification App Server Using Web Deploy](#GettingStarted)
*	[Exercise 1: Configure a Windows 8 Style UI Client application for Push Notifications](#Exercise1)
*	[Exercise 2: Sending Push Notifications](#Exercise2)

Estimated time to complete this lab: **45 minutes**.

<a name="GettingStarted" />
### Getting Started: Deploying the Notification App Server Using Web Deploy###

In this exercise, you deploy the notification app server to Windows Azure using Web Deploy. To do this, you provision the required service components at the management portal, request credentials from the WNS and Live Connect portal and deploy to Windows Azure using Web Deploy.   

<a name="GSTask1" />
#### Task 1 – Creating a Storage Account and Web Site ####

The application you deploy in this exercise requires a Web Site and a Storage Account. In this task, you create a new storage account to allow the application to persist its data. In addition, you define a Web Site to host the notification app server.

1. Navigate to [<https://manage.windowsazure.com>](<https://manage.windowsazure.com>) using a Web browser and sign in using the Microsoft Account associated with your Windows Azure account.


	![Signing in to the Windows Azure platform Management portal](images/signing-in-to-the-windows-azure-platform-mana.png?raw=true)

	_Signing in to the Windows Azure platform Management portal_


1. First, you will create the **Storage Account** that the application will use to store its data. In the Windows Azure Management Portal, click **New** | **Storage Account** | **Quick Create**.

1. Set a unique **URL**, for example _myNotificationAppServer_, and click the **Tick** to continue.
 
	![Creating a new storage account](images/creating-a-new-storage-account.png?raw=true)

	_Creating a new storage account_

	> **Note:** The URL used for the storage account corresponds to a DNS name and is subject to standard DNS naming rules. Moreover, the name is publicly visible and must therefore be unique. The portal ensures that the name is valid by verifying that the name complies with the naming rules and is currently available. A validation error will be shown if you enter a name that does not satisfy the rules.
	>
	> ![URL Validation](images/url-validation.png?raw=true)

1. Wait until the Storage Account is created. Click your storage account's name to go to its **Dashboard**.

	![Storage Accounts page](images/storage-accounts-page.png?raw=true "Storage Accounts page")

	_Storage Accounts page_

1.	In the **Dashboard** page, you will see the **URL** assigned to each service in the storage account. Record the public storage account name, this is the first segment of the URL assigned to your endpoints.

	![Storage Account Dashboard page](images/storage-account-dashboard-page.png?raw=true "Storage Account Dashboard page")

	_Storage Account Dashboard page_

1.	Click **Manage Keys** at the bottom of the page in order to show the storage account's access keys.

1. Copy the **Primary access key** value. You will use this value later on to configure the application.

	![Manage Storage Account Keys](images/manage-storage-account-keys.png?raw=true "Manage Storage Account Keys")

	_Manage Storage Account Keys_


	>**Note:** The **Primary Access Key** and **Secondary** Access **Key** both provide a shared secret that you can use to access storage. The secondary key gives the same access as the primary key and is used for backup purposes. You can regenerate each key independently in case either one is compromised.

1. Go back to the portal home page, and select **Web Sites**.

1. Select **New**, then select **Web Site** from the list and then **Quick Create**.

1. Choose a name for your Web Site and then select **Create Web Site**

	![Creating a new Web Site](images/creating-a-new-web-site.png?raw=true)
	
	_Creating a new Web Site_


<a name="GSTask2" />
#### Task 2 – Updating the application with your Storage Account Name and Key ####
In this task, you will update the Connection String values within the web configuration file using the Storage Account you created in the previous task.

1. In a new instance of **Visual Studio 2012**, open **Services.sln** located in the **Assets** folder.  This is the Notification App Server.

1. Open **Web.config**.  In the **appsettings** section, replace the value of the **DataConnectionString** setting using the recommended format as shown in the commented lines in the snippet below.

	````XML
	<!--
		  When deploying to Windows Azure, replace the DataConnectionString setting with the
		  connection string for your Windows Azure Storage account. For example:
		  "DefaultEndpointsProtocol=https;AccountName={your storage account name};AccountKey={your storage account key}"
		  -->
		<add key="DataConnectionString" value="DefaultEndpointsProtocol=https;AccountName={your storage account};AccountKey={your storage account key}"/


	````
1. Save your changes.

1. Open the **Package Manager Console**, and in the Package Sources window add the subfolders located in the **Assets/Server/Nugets** folder as new sources.

1. Change the Package source in the Package Manager Console to **All**.

1. Build the solution.

<a name="GSTask3" />
#### Task 3 – Requesting WNS Credentials and updating the Web.config ####
In this task, you will obtain the Windows Push Notification Services (WNS) credentials and use them to update the Web Configuration file.

1.	To request **WNS** credentials you will require your publisher credentials for your Windows 8 Style UI app.  In a new instance of **Visual Studio 2012**, open your existing HTML5/JS Windows 8 Style UI application or create a new application. 

	> **Note:**  If you do not have an existing client application for step 1 in Visual Studio 2012 for Windows 8 Express you can use **File** | **New Project** | Select **Templates** | **Javascript** and then **Blank Application**. Press **OK**.

1.	In solution explorer open your **package.appxmanifest** and select the **packaging** tab.  We will use the **Package Display Name** and **Publisher** fields for creating your **WNS** Credentials.

	![Opening package.appxmanifest](images/opening-packageappxmanifest.png?raw=true)

	_Opening package.appxmanifest_

1.	Navigate to the **Windows Push Notifications & Live Connect** portal (http://manage.dev.live.com/build).

	![Login to request WNS credentials](images/login-to-request-wns-credentials.png?raw=true)

	_Login to request WNS credentials_

1.	Sign in using your **Microsoft Account**.
1.	Follow the **Step 1** and **Step 2** provided in the portal to supply your Package Name and Certificate Name (CN).

	![Requesting WNS Credentials](images/requesting-wns-credentials.png?raw=true)

	_Requesting WNS Credentials_

	> **Note:**  Make sure you have copied the publisher to the portal correctly, otherwise, you will get a 403 unauthorized error when trying to send notifications.

	![Credentials supplied for Auth against WNS](images/credentials-supplied-for-auth-against-wns.png?raw=true)

	_Credentials supplied for Auth against WNS_

	>**Note:** Keep this browser open until the end of the lab as it can be used in subsequent steps.  If you prefer to close the browser, copy the three credentials to notepad for later use.

1.	Switch to the Notification App Server, open **Web.config** file and replace _[YOUR_WNS_PACKAGE_SID]_ with the **Package Security Identifier (SID)**  and _[YOUR_WNS_CLIENT_SECRET]_ with the **Client secret**.

	![Updating Web.config with WNS Credentials](images/updating-webconfig-with-wns-cred.png?raw=true)

	_Updating Web.config with WNS Credentials_

	> **Note:** Ensure you have not copied a white space on the start or end of the values in the **Web.config** and that the **Package SID** and **Client Secret** were pasted into the correct fields.

1.	Your Notification App Server is now ready to deploy to Windows Azure. Note if your account is limited to one core.


<a name="GSTask4" />
#### Task 4 – Deploy your Notification App Server to Windows Azure using Web Deploy####

In this task, you will deploy the Notification App Server to Windows Azure using Web Deploy.

1. In the Windows Azure Portal, select **Web Sites**, and then select your Web Site to open the **Dashboard**.  In the **Dashboard** page, under the **quick glance** section, click the **Download publish profile** link and save the file to a known location. You will use theses settings later to publish the web site from Visual Studio 2012.

	> **Note:** The _publish profile_ contains all of the information required to publish a web application to a Windows Azure website for each enabled publication method. The publish profile contains the URLs, user credentials and database strings required to connect to and authenticate against each of the endpoints for which a publication method is enabled. **Microsoft Visual Studio** supports reading publish profiles to automate the publishing configuration for web applications to Windows Azure Web Sites.

	![Downloading the publish profile](images/download-publish-profile.png?raw=true "Downloading the publish profile")

	_Downloading the publish profile_


1. In Visual Studio's Solution Explorer, right-click the **Notification App Server** project node and select **Publish** to open the Publish Web wizard.

	![Publishing the service](images/publishing-the-service.png?raw=true "Publishing the service")

	_Publishing the service_

1. In the **Profile** page, click the **Import** button and select your publishing profile file. Click **Next**.

	![Publising profile profile selection](images/publishing-profile-profile-selection.png?raw=true)
		
	_Selecting a publishing profile file_

1. In the **Connection page**, leave the imported values and click **Next**.

	![Publishing profile imported](images/publishing-profile-imported.png?raw=true "Publishing profile imported")

	_Publishing profile imported_

1. In the **Settings** page, leave the default values and click **Next**.

	![Publishing profile, Settings page](images/publishing-profile-settings-page.png?raw=true "Publishing profile, Settings page")

	_Publishing profile - Settings_

1. In the **Preview** page, click **Publish**.

	![Publishing Profile - Preview Page](images/publishing-profile-preview-page.png?raw=true "Publishing Profile - Preview Page")

	_Publishing Profile - Preview Page_

	>**Note:** If this is the first time you deploy the web site, you will be prompted to accept a certificate. After the message appears, click **Accept**. 
	>
	> ![Publishing certificate error](images/publishing-certificate-error.png?raw=true "Publishing certificate error")
	>

	

<a name="Exercise1" />
### Exercise 1: Configure a Windows 8 Style UI Client application for Notifications ###

In this exercise, you will configure your client application to request a notification channel from the WNS and register this channel with your Notification App Server running in Windows Azure.

<a name="Ex1Task1" />
#### Task 1 – Configuring the package.appmanifest for Push Notifications ####

In this task, you will update the package.appmanifest to receive Wide Tile notifications using Visual Studio 2012.

1.	Return to your Windows 8 Style UI client application in **Visual Studio 2012**. 
1.	In **Solution Explorer** double click **package.appmanifest**.

	![Updating your client app with WNS credentials](images/updating-your-client-app-with-wns-credentials.png?raw=true)

	_Updating your client app with WNS credentials_

1.	In order to enable your application to receive **Wide Tile** notifications, click **Browse** on Wide Logo and navigate to the **Assets/Client** folder. Then, select **widelogo.png** and click **Open**.

	![Adding a wide logo to your application](images/adding-a-wide-logo-to-your-application.png?raw=true)

	_Adding a wide logo to your application_

1.	Scroll down and change the **Toast Capable** dropdown to be _Yes_.

	![Configuring your client application to allow Toast Notifications](images/configuring-your-client-application-to-allow.png?raw=true)

	_Configuring your client application to allow Toast Notifications_

1.	Select the **Packaging** tab of **package.appmanifest**.
1.	Update the **Package Name** to the Package name created during the prior task in the **WNS & Live Connect Portal** (https://manage.dev.live.com/build) as depicted in the following figure. 

	![Configuring your package.appmanifest package name](images/configuring-your-packageappmanifest-package-n.png?raw=true)

	_Configuring your package.appmanifest package name_

1.	**Close** and **Save** changes to **package.appmanifest**.

<a name="Ex1Task2" />
#### Task 2 – Updating the Client Codebase for Notifications ####

In this task, you will update your client application to be able to send push notifications using the Notification App Server.

1.	In Solution Explorer **Right click** on the **js** folder and select add existing item.
1.	Browse to the **Assets/Client**  folder, select **notifications.js** and click **Add**.

	![Add notifications.js to your client application](images/add-notificationsjs-to-your-client-applicatio.png?raw=true)

	_Add notifications.js to your client application_

1. Open **notifications.js** file and update the url to point to the correct endpoint.  Update the **serverUrl** value _[YOUR_DNS_NAME]_ in http://_[YOUR_WEBSITE_DOMAIN]_/endpoints.  You obtain the **DNS** value from the web site you created in the **Winows Azure Management Portal** in the previous exercise.

	`var serverUrl = "http://[YOUR_WEBSITE_DOMAIN]/endpoints";`


1.	Open **default.html** within the Solution Explorer and add a **script reference** to _/js/notifications.js_ and a **div** tag with id _statusMessage_.

	<!-- mark:15,18 -->
	````HTML
	<!DOCTYPE html>
	<html>
	<head>
		 <meta charset="utf-8">
		 <title>Application21</title>

		 <!-- WinJS references -->
		 <link href="//Microsoft.WinJS.0.6/css/ui-dark.css" rel="stylesheet">
		 <script src="//Microsoft.WinJS.0.6/js/base.js"></script>
		 <script src="//Microsoft.WinJS.0.6/js/ui.js"></script>

		 <!-- Application21 references -->
		 <link href="/css/default.css" rel="stylesheet">
		 <script src="/js/default.js"></script>
		 <script src="/js/notifications.js"></script>
	</head>
	<body>
		  <div id="statusMessage"></div>
	</body>
	</html>
	````

1.	Open **default.js** within **js** folder, and add a call to the **openNotificationsChannel()** of **notifications.js**.

	![Adding a call to openNotificationsChannel() to ensure your channel is requested from WNS and Registered with your Notification App Server](images/adding-a-call-to-opennotificationschannel-to.png?raw=true)

	_Adding a call to openNotificationsChannel() to ensure your channel is requested from WNS and Registered with your Notification App Server_

1.	Save all the changes from **File** | **Save All** (or by pressing **Ctrl + Shift + S**).
1.	In the **Build** menu, click **Build Solution** to ensure your builds.
1.	Open **notifications.js** and locate the **openNotificationsChannel()** method. This method will create a Push Notification Channel for the Notification App Server.

<a name="Exercise2" />
### Exercise 2: Sending Push Notifications ###

This section describes how to run your client application and send notifications to it through the notification app server deployed to Windows Azure in the previous exercises.  

<a name="Ex2Task1" />
#### Task 1 – Confirm your Web Site deployment to Windows Azure is complete ####

In this task, you will verify that your application was correctly deployed to Windows Azure.

1.	Navigate to your deployed web site http://**\<notificationsappserver\>**.cloudapp.net.

	![Notification App Server portal running in Windows Azure](images/notification-app-server-portal-running-in-win.png?raw=true)

	_Notification App Server portal running in Windows Azure_


<a name="Ex2Task2" />
#### Task 2 – Running the Notification enabled Windows 8 Style UI App ####

In this task, you will run the client application you created in the previous exercise to create a channel for the WNS and register it with the Notification App Server.

1.	Return to your Windows 8 Style UI App where you configured the notifications.
1.	Once the solution has opened press **F5**. Due the configuration you made previously when the application launches it will call **openNotificationsChannel()** method. This will request a channel from **WNS** and submit it to the **Notifications App Server** you deployed to Windows Azure.  In the **statusMessage** div, you will see that the **Channel URI** was sent successfully to your service.

	![Client output after successful channel request from WNS and registering with notification app server](images/client-output-after-successful-channel-reques.png?raw=true)

	_Client output after successful channel request from WNS and registering with notification app server_

<a name="Ex2Task3" />
#### Task 3 – Sending Push Notifications using the ASP .NET MVC 4 Portal ####

Now that a channel has been successfully requested from WNS and registered with your Notification App Server we can now start to send notifications through the portal.

1.	Switch to the Web browser and log into your deployed application (e.g: http://_[YOUR_SUBDOMAIN]_/.cloudapp.net), using the following credentials:
	1. User Name: **admin**
	1. Password: ![password](images/password.png?raw=true) (with a zero)

1.	Once logged in, additional menu options are displayed to allow you to send push notifications and manage the images (blobs) used when sending notifications.

	![Notification App Server Home Page](images/notification-app-server-home-page.png?raw=true)

	_Notification App Server Home Page_

1.	Click the **Push Notifications** menu option. Here you will see the channel that you requested using the sample Windows 8 Style UI app that registered with your Web Role.

	![Pushing Notifications](images/pushing-notifications.png?raw=true)
	
	_Pushing Notifications_

	> **Note:** If you re-register your channel from your client app while this page is open it is worthwhile refreshing the page to ensure that you have captured the latest channel uri.

1.	You can now send your first _toast_ notification to this channel. Click **Send Notification** button to open the notifications template dialog window.
1.	 Select **Toast** in the first drop-down list and then select the **ToastImageAndText01** template.
1.	Configure the template column with your Square **WindowsAzureLogo.png** and type some text into **Regular text** as per below:

	![Selecting notification type and template](images/selecting-notification-type-and-template.png?raw=true)

	_Selecting notification type and template_

1.	Click **Send** and observe that the Web portal indicates that the message was successfully delivered to **WNS** and the _Toast_ notification arrives to the Client application.

	![Notification sent confirmation](images/notification-sent-confirmation.png?raw=true)

	_Notification sent confirmation_

	>**Note:**  If you are getting a 403 unauthorized or no notifications showing up please try refreshing the page to ensure that the channel that you are sending to is refreshed.  Following this, please check that you created your WNS credentials with the correct CN from your package.appxmanifest and that you have configured your package.appxmanifest and Web.config correctly.

1.	Now you will see how to send a _Tile_ notification. Select **Tile** in the first drop-down and then select the **TileWideImageAndText01** template.
1.	Configure the template column with:
	1.	Your wide logo **WindowsAzureLogoWide.png**.
	1.	Custom text to be displayed in **Large Text** and **Regular Text**.
	1.	Click **Send**.

		![Pushing a Tile notification](images/pushing-a-tile-notification.png?raw=true)

		_Pushing a Tile notification_

1.	Return to **Start** by pressing the **Windows key** ( ![start](images/start.png?raw=true)) and observe that your _Tile_ notification has now been delivered and is being displayed.

	![Delivered Tile notification](images/delivered-tile-notification.png?raw=true)
	
	_Delivered Tile notification_

1.	Now you will see how to send a _Badge_ notification. Select **Badge** in the first drop-down list and then select the **Glyph** template.
1.	In the template column select the **NewMessage** option and click **Send**.

	![Pushing a Badge notification](images/pushing-a-badge-notification.png?raw=true)

	_Pushing a Badge notification_

1.	Return to **Start** by pressing the **Windows key** ( ![start](images/start.png?raw=true)) and observe that your _Tile_ has now the _Badge_ updated to see the NewMessage Glyph type:

	![Updated Tile notification with a Badge](images/updated-tile-notification-with-a-badge.png?raw=true)

	_Updated Tile notification with a Badge_

	>**Note:** This concludes the overview of how to send Toast, Tile and Badge notifications using the Windows Azure Toolkit for Windows 8. As an exercise, it’s recommended to spend some time both exploring the rich set of templates available to each of the different notification types and how you can use blob storage to store your image assets for notifications.

---

<a name="Summary" />
## Summary ##
By completing this Hands-On Lab you have learned how to:

-	 Use the Windows Azure Management Portal to create storage accounts and web site components.
-	Use the WNS and Live Connect Portal to request credentials for use with WNS.
-	Deploy a web site using Web Deploy.
-	Configure a Windows 8 Style UI client to receive notifications.
-	Test sending notifications to your client app via WNS using the Windows Azure Toolkit for Windows 8 portal.

If you would like the full codebase for the **Notification App Server** to update for your own applications please download the **Windows Azure Training Kit for Windows 8** (http://watwindows8.codeplex.com).