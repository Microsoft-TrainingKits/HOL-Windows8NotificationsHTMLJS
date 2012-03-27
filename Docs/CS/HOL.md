<a name="HOLTop" />
# Sending Push Notifications using Windows Azure and the Windows Push Notification Service #
---

<a name="Overview" />
## Overview ##
In this hands-on lab, you will learn how to deploy a pre-packaged version of the [Windows Azure Toolkit for Windows 8](http://watwindows8.codeplex.com/) to Windows Azure and then utilize this deployment to send notifications to your client application via the [Windows Push Notification Service (WNS)] (http://msdn.microsoft.com/en-us/library/windows/apps/hh465460\(v=vs.85\).aspx). By the end of this lab you will have a fully functional portal capable of sending Toast, Tile and Badge notifications to your Windows Metro Style client application.

![Windows Azure Toolkit for Windows 8 delivering a notification via WNS](images/windows-azure-toolkit-for-windows-8-deliverin.png?raw=true)

_Windows Azure Toolkit for Windows 8 delivering a notification via WNS_

If you would like to learn more about how this lab works please see [this video](http://channel9.msdn.com/Events/TechDays/Techdays-2012-the-Netherlands/2294).

<a name="Objectives" />
### Objectives ###

In this hands-on lab, you will learn how to:

-	Use the Windows Azure platform Management Portal to create storage accounts and hosted service components.
-	Use the Windows Push Notification and Live Connect Portal to request credentials for use with WNS.
-	Deploy service component packages using the Windows Azure Platform Management Portal user interface.
-	Configure a Windows Metro Style client to receive notifications
Test sending notifications to your client app via WNS using the Windows Azure Toolkit for Windows 8 portal.

<a name="SystemRequirements" />
### System Requirements ###

You must have the following items to complete this lab:

-	[Windows 8 Consumer Preview][1].
-	[Visual Studio 11 Express Beta for Windows 8][2] or greater.
-	A Windows Azure subscription
	- If you are attending a Lab please ask you Lab Proctor if they are supplying Windows Azure account keys and then activate your account using http://windowsazurepass.com, otherwise please use the free trial method below. 
	- Or, please register for a 90 day no obligation [Free Windows Azure Trial][3]. 

[1]:http://windows.microsoft.com/en-US/windows-8/download
[2]:http://msdn.microsoft.com/en-us/windows/apps/hh852659
[3]:http://www.microsoft.com/windowsazure/free-trial

<a name="Setup" />
### Setup ###
In order to execute the exercises in this hands-on lab you need to set up your environment.

1. Open a Windows Explorer window and browse to the lab’s **source** folder.

1. Double-click the **Setup.cmd** file in this folder to launch the setup process that will configure your environment.

1. If the User Account Control dialog is shown, confirm the action to proceed.

>**Note:** Make sure you have checked all the dependencies for this lab before running the setup.

---

<a name="Exercises" />
## Exercises ##
This hands-on lab includes the following exercises:

1.	[Deploying an Application Using the Windows Azure Platform Management Portal](#Exercise1)
1.	[Configure a Windows Metro Style Client application for Push Notifications](#Exercise2)
1.	[Sending Push Notifications](#Exercise2)

Estimated time to complete this lab: **45 minutes**.

<a name="Exercise1" />
### Exercise 1: Deploying an Application Using the Windows Azure Platform Management Portal ###

In this exercise, you deploy the notification app server to Windows Azure using the Windows Azure Platform Management Portal. To do this, you provision the required service components at the management portal, request credentials from the WNS and Live Connect portal, configure and upload the package to Windows Azure.   

<a name="Ex1Task1" />
#### Task 1 – Creating a Storage Account and a Hosted Service Component ####

The application you deploy in this exercise requires both compute and storage services. In this task, you create a new Windows Azure storage account to allow the application to persist its data. In addition, you define a hosted service component to execute application code.

1.	This step is only required if you do not already have a Windows Azure account.
	1.	If you are attending a Lab please ask you Lab Proctor if they are supplying Windows Azure account keys and then activate your account using http://windowsazurepass.com otherwise please use the free trial method below. 
	1. Or, please register for a 90 day no obligation [Free Windows Azure Trial](http://www.microsoft.com/windowsazure/free-trial).
    
1.	Navigate to http://windows.azure.com and sign in using the Windows Live ID associated with your Windows Azure account.

	![Signing in to the Windows Azure Platform Management portal](images/signing-in-to-the-windows-azure-platform-mana.png?raw=true)

	_Signing in to the Windows Azure Platform Management portal_

1.	First, you create the storage account that the application will use to store its data. In the Windows Azure ribbon, click **New Storage Account**.
 
	![Creating a new storage account](images/creating-a-new-storage-account.png?raw=true)

	_Creating a new storage account_

1.	In the **Create a New Storage Account** dialog, pick your subscription in the drop down list labeled **Choose a subscription**.

	![Choosing a subscription to host the storage account](images/choosing-a-subscription-to-host-the-storage-a.png?raw=true)

	_Choosing a subscription to host the storage account_

1.	In the textbox labeled **Enter a URL**, enter the name for your storage account, for example, **\<NotificationAppServer\>**, where _\<NotificationAppServer\>_ is a unique name. Windows Azure uses this value to generate the endpoint URLs for the storage account services.

	![Choosing the URL of the new storage account](images/choosing-the-url-of-the-new-storage-account.png?raw=true)

	> **Note:**  The name used for the storage account corresponds to a DNS name and is subject to standard DNS naming rules. Moreover, the name is publicly visible and must therefore be unique. The portal ensures that the name is valid by verifying that the name complies with the naming rules and is currently available. A validation error will be shown if you enter a name that does not satisfy the rules.
	> ![URL Validation](images/url-validation.png?raw=true)

1.	Select the option labeled **Create or choose an affinity group** and then pick **Create a new affinity group** from the drop down list.

	![Creating a new affinity group](images/creating-a-new-affinity-group.png?raw=true)

	_Creating a new affinity group_

	>**Note:** The reason that you are creating a new affinity group is to deploy both the hosted service and storage account to the same location, thus ensuring high bandwidth and low latency between the application and the data it depends on.

1.	In the **Create a New Affinity Group** dialog, enter an **Affinity Group Name**, select its **Location** in the drop down list, and then click **OK**.

	![Configuring a new affinity group](images/configuring-a-new-affinity-group.png?raw=true)

	_Configuring a new affinity group_

1.	Back in the **Create a New Storage Account** dialog, click **Create** to register your new storage account. Wait until the account provisioning process completes and updates the **Storage Accounts** tree view. Notice that the **Properties** pane shows the **URL** assigned to each service in the storage account. Record the public storage account name-this is the first segment of the URL assigned to your endpoints.

	![Storage account successfully created](images/storage-account-successfully-created.png?raw=true)

	_Storage account successfully created_

1.	Now, click the **View** button next to **Primary access key** in the **Properties** pane. In the **View Storage Access Keys** dialog, click **Copy to Clipboard** next to the **Primary Access Key**. You will use this value later on to configure the application.

	![Retrieving the storage access keys](images/retrieving-the-storage-access-keys.png?raw=true)

	_Retrieving the storage access keys_

	>**Note:** The **Primary Access Key** and **Secondary** Access **Key** both provide a shared secret that you can use to access storage. The secondary key gives the same access as the primary key and is used for backup purposes. You can regenerate each key independently in case either one is compromised.

1. Next, create the compute component that executes the application code. Click **Hosted Services** on the left pane. Click on **New Hosted Service** button on the ribbon.

	![Creating a new hosted service](images/creating-a-new-hosted-service.png?raw=true)

	_Creating a new hosted service_

1.	In the **Create a new Hosted Service** dialog, select the subscription where you wish to create the service from the drop down list labeled **Choose a subscription**.

	![Choosing your subscription](images/choosing-your-subscription.png?raw=true)

	_Choosing your subscription_

1.	Enter a service name in the textbox labeled **Enter a name for your service** and choose its URL  by entering a prefix in the textbox labeled  **Enter a URL prefix for your service**, for example, **\<notificationappserver\>**, where _\<notificationappserver\>_ is a unique name. Windows Azure uses this value to generate the endpoint URLs for the hosted service.

	![Configuring the hosted service URL and affinity group](images/configuring-the-hosted-service-url-and-affini.png?raw=true)

	_Configuring the hosted service URL and affinity group_

	>**Note:** If possible, choose the same name for both the storage account and hosted service. However, you may need to choose a different name if the one you select is unavailable.
	>
	>The portal ensures that the name is valid by verifying that the name complies with the naming rules and is currently available. A validation error will be shown if you enter name that does not satisfy the rules.
	>
	>![URL prefix validation](images/url-prefix-validation.png?raw=true)

1.	Select the option labeled **Create or choose an affinity group** and then pick the affinity group you defined when you created the storage account from the drop down list.

	![Choosing an affinity group](images/choosing-an-affinity-group.png?raw=true)

	>**Note:** By choosing this affinity group, you ensure that the hosted service is deployed to the same data center as the storage account that you provisioned earlier.

1.	Select the option labeled **Deploy not Deploy**.

	>**Note:** While you can create and deploy your service to Windows Azure in a single operation by completing the **Deployment Options** section, for this hands-on lab, you will defer the deployment step until we have finished the configuration.

1.	Click **OK** to create the hosted service and then wait until the provisioning process completes.

	![Hosted service successfully created](images/hosted-service-successfully-created.png?raw=true)

	_Hosted service successfully created_

1.	Do not close the browser window. You will use the portal for the next task.

<a name="Ex1Task2" />
#### Task 2 – Updating the ServiceConfiguration.cscfg with your Storage Account Name and Key ####

In this task, you will update the Connection String values within the ServiceConfiguration file using the Storage Account you created in the previous task.

1.	Next, Open **/Server/ServiceConfiguration.cscfg** 

	![Configuring the storage account connection strings](images/configuring-the-storage-account-connection-st.png?raw=true)

	_Configuring the storage account connection strings_

1.	Replace the placeholder labeled _[YOUR_ACCOUNT_NAME]_ with the Windows Azure Storage **Account Name** that you created earlier.
1.	Replace the placeholder labeled _[YOUR_ACCOUNT_KEY]_ with the Windows Azure Storage account **Primary Access Key** value that you created earlier, when you created the storage account in Task 1. Again, replace both instances of the placeholder, one for each connection string.

	![Setting the storage account connection strings](images/setting-the-storage-account-connection-string.png?raw=true)

	_Setting the storage account connection strings_

	>**Note:** The connection string text has been wrapped for the purposes of this screenshot only.  Leave as one line in your real config.

<a name="Ex1Task3" />
#### Task 3 – Requesting WNS Credentials and updating the ServiceConfiguration.cscfg ####

In this task, you will obtain the Windows Push Notification Services (WNS) credentials and use them to update the ServiceConfiguration file.

1.	To request **WNS** credentials you will require your publisher credentials for your metro style app.  Launch **Visual Studio 11 Beta** and open your existing C#/XAML Metro Style application or create a new application. 

	>**Note:**  If you do not have an existing client application for step 1 in Visual Studio 11 Beta for Windows 8 Express you can use **File** | **New Project** | Select **Templates** | **Visual C#** and then **Blank Application**. Press **OK**.

1.	In solution explorer open your **package.appxmanifest** and select the **packaging** tab.  We will use the **Package Display Name** and **Publisher** fields for creating your WNS Credentials.

	![Opening package.appxmanifest](images/opening-packageappxmanifestcsharp.png?raw=true "Opening package.appxmanifest")

	_Opening package.appxmanifest_

1.	Navigate to the Windows Push Notifications and Live Connect portal (http://manage.dev.live.com/build).

	![Login to request WNS credentials](images/login-to-request-wns-credentials.png?raw=true)

	_Login to request WNS credentials_

1.	Sign in using your **Windows Live ID**.
1.	Follow the **Step 1** and **Step 2** provided in the portal to supply your package name and CN.

	![Requesting WNS Credentials](images/requesting-wns-credentials.png?raw=true)

	_Requesting WNS Credentials_

	>**Note:**  It’s Important to ensure you have copied the publisher to the portal correctly as you will get a 403 unauthorized error when trying to send notifications if you have done this step incorrectly.

	![Credentials supplied for Auth against WNS](images/credentials-supplied-for-auth-against-wns.png?raw=true)

	_Credentials supplied for Auth against WNS_

	>**Note:** Keep this browser open until the end of the lab as it can be used in subsequent steps.  If you prefer to close the browser, copy the three credentials to notepad for later use.

1.	Open **/Server/ServiceConfiguration.cscfg** and replace **[YOUR_WNS_PACKAGE_SID]** with the Package Security Identifier (SID)  and the **[YOUR_WNS_CLIENT_SECRET]** with the Client secret.

	![Updating ServiceDefinition.cscfg with WNS Credentials](images/updating-servicedefinitioncscfg-with-wns-cred.png?raw=true)

	_Updating ServiceDefinition.cscfg with WNS Credentials_

	>**Note:** Ensure you have not copied white space on the start or end of the values in the **ServiceDefinition.cscfg** and that the **Package SID** and **Client Secret** were pasted into the correct fields.

1.	Your Notification App Server is now ready to deploy to Windows Azure.   Note if your account is limited to one core.

	>**Note:** If your account is limited to one core only you can update the Instances element to set the count to 1 in the **ServiceDefinition.cscfg**.

<a name="Ex1Task4" />
#### Task 4 – Deploy your Notification App Server to Windows Azure ####

In this task, you will deploy the Notification App Server ASP.NET MVC application to Windows Azure using the Windows Azure Management Portal.

1.	Go back to the **Windows Azure Management Portal** (https://windows.azure.com) and select **Hosted Services** section from the left pane.
1.	At the portal, select the hosted service you created in the previous step and then click **New Production Deployment** on the ribbon. 

	>**Note:** A hosted service is a service that runs your code in the Windows Azure environment. It has two separate deployment slots: staging and production. The staging deployment slot allows you to test your service in the Windows Azure environment before you deploy it to production.  For this demo we will deploy straight to production.

	![Hosted service summary page](images/hosted-service-summary-page.png?raw=true)

	_Hosted service summary page_

1.	In the **Create a new Deployment** dialog click **Browse Locally** to select a **Package location** and navigate to the **Assets/Server** folder where you performed the configuration in the prior task and then select **WindowsAzureAppServer.cspkg**.

	>**Note:** The _.cspkg_ file is an archive file that contains the binaries and files required to run a service - in this case it’s the Notification App Server ASP.NET MVC application. We used Visual Studio to create the service package for you using **Build | Publish** for the Windows Azure project.

1.	Now, to choose the **Configuration File**, click **Browse Locally** and select **/Server/ServiceConfiguration.cscfg** in the same **/Server** folder that you used in the previous step.

	>**Note:** The _.cscfg_ file contains configuration settings for the application, including the instance count and configuration for WNS and Windows Azure Storage that you performed in the previous exercise.

1.	Finally, for the **Deployment name**, enter a label to identify the deployment; for example, use **v1.0**.

	>**Note:** The management portal displays the label in its user interface for staging and production, which allows you to identify the version currently deployed in each environment.

	![Configuring service package deployment](images/configuring-service-package-deployment.png?raw=true)

	_Configuring service package deployment_

1.	Click **OK** to start the deployment. 
1.	Click **Yes** to override and submit the deployment request. Notice that the package begins to upload and that the portal shows the status of the deployment to indicate its progress.

	![Uploading a service package to the Windows Azure Platform Management Portal](images/uploading-a-service-package-to-the-windows-az.png?raw=true)

	_Uploading a service package to the Windows Azure Platform Management Portal_

1.	While you wait for the deploy process to finish, as depicted in the diagram below, you can continue with the next exercise to configure your client application to deliver notifications using your new Notification App Server.

	>**Note:** During deployment, Windows Azure analyzes the configuration file and copies the service to the correct number of machines, and starts all the instances. Load balancers, network devices and monitoring are also configured during this time.

	![Package successfully deployed](images/package-successfully-deployed.png?raw=true)

	_Package successfully deployed_

<a name="Exercise2" />
### Exercise 2: Configure a Windows Metro Style Client application for Notifications ###

In this exercise, you will configure your client application to request a notification channel from the WNS and register this channel with your Notification App Server running in Windows Azure.

<a name="Ex2Task1" />
#### Task 1 – Configuring the package.appmanifest for Push Notifications ####

In this task, you will update the package.appmanifest to receive Wide Tile notifications using Visual Studio 11 Express.

1.	Return to your Windows Metro Style client application in **Visual Studio 11 Express**. 
1.	In **Solution Explorer** double click **package.appmanifest**.

	![Updating your client app with WNS credentials](images/updating-your-client-app-with-wns-credentialscsharp.png?raw=true "Updating your client app with WNS credentials")

	_Updating your client app with WNS credentials_

1.	To enable your application to receive Wide Tile notifications Click **Browse** on Wide Logo, navigate to the **/Client**  folder and select /Client/**widelogo.png**.

	![Adding a wide logo to your application](images/adding-a-wide-logo-to-your-applicationcsharp.png?raw=true "Adding a wide logo to your application")

	_Adding a wide logo to your application_

1.	Press **Open**.
1.	Scroll down and change the **Toast Capable** dropdown to be **Yes.**

	![Configuring your client application to allow Toast Notifications](images/configuring-your-client-application-csharp.png?raw=true "Configuring your client application to allow Toast Notifications")

	_Configuring your client application to allow Toast Notifications_

1.	Select the **Packaging Tab** of package.appmanifest.
1.	Update the Package Name to the Package name created during the prior task in the WNS and Live Connect Portal (https://manage.dev.live.com/build) as depicted in the following figure. 

	![Configuring your package.appmanifest package name](images/configuring-your-packageappmanifest-package-ncsharp.png?raw=true)

	_Configuring your package.appmanifest package name_

1.	**Close** and **Save** changes to **package.appmanifest**.

<a name="Ex2Task2" />
#### Task 2 – Updating the Client Codebase for Notifications ####

In this task, you will update your client application to be able to send push notifications using the Notification App Server.

1.	In Solution Explorer **Double click** on **BlankPage.xaml** and insert the highlighted code into the Grid element.

	<!-- mark: 2-4 -->
	````XAML
	<Grid Background="{StaticResource ApplicationPageBackgroundBrush}">
		<TextBlock Name="txtStatusMessage" HorizontalAlignment="Left" TextWrapping="Wrap" Text="Status Messages" VerticalAlignment="Top" Height="181" Width="1346" Margin="10,10,0,0" FontSize="20"  />

		<Button Name="btnRequestAndRegister" Content="Request And Register Channel" VerticalAlignment="Top" Width="234" Margin="10,213,0,0" Click="btnRequestAndRegister_Click" />

		<Button Name="btnUnregister" Content="Unregister" HorizontalAlignment="Left" Margin="249,213,0,0" VerticalAlignment="Top" Width="123" Click="btnUnregister_Click"/>
	</Grid>
	````


1.	In the file explorer navigate to **/Client** and open **/Client/BlankPageNotificationSnipper.txt**.

1.	Copy the contents of BlankPageNotificationSnippet.txt and paste it over the content within the  BlankPage class declaration contained within BlankPage.xaml.cs per the following figure

	![Updating BlankPage.xaml.cs](images/updating-blankpagexamlcs.png?raw=true "Updating BlankPage.xaml.cs")

	_Updating BlankPage.xaml.cs_

1.	Within **BlankPage.xaml.cs** 
	- Add the following using statements
		
		<!-- mark:1,3 -->
		````C#
		using Windows.Networking.PushNotifications;
	using System.Threading.Tasks;
	using System.Net.Http;
	````

		![Add using Windows.Networking.PushNotifications](images/add-using-windowsnetworkingpushnotifications.png?raw=true "Add using Windows.Networking.PushNotifications")

		_Add using Windows.Networking.PushNotifications_

	- Update **[YOUR_SUBDOMAIN]** in http:// **[YOUR_SUBDOMAIN]**.cloudapp.net/ChannelRegistrationService to use the dns prefix from the DNS Name in Windows Azure Management Portal (<https://windows.azure.com> ) that you configured in the previous exercise a per the figure below:


		![Set the DNS prefix of your hosted service](images/set-the-dns-prefix-of-your-hosted-servicecsharp.png?raw=true "Set the DNS prefix of your hosted service")

		_Set the DNS prefix of your hosted service_

1.	Press **File** then **Save All**.

1.	Press **Build** then **Build Solution** to ensure your builds.
1.	Locate and review the **OpenNotificationsChannel()** method of **BlankPage.xaml**. This method will create a Push Notification Channel for the Notification App Server.

<a name="Exercise3" />
### Exercise 3: Sending Push Notifications ###

This section describes how to run your client application and send notifications to it through the notification app server deployed to Windows Azure in the previous exercises.  

<a name="Ex3Task1" />
#### Task 1 – Confirm your Hosted Service deployment to Windows Azure is complete ####

In this task, you will verify that your application was correctly deployed to Windows Azure.

1.	Navigate to your deployed hosted service http://**\<notificationsappserver\>.**cloudapp.net.

	![Notification App Server portal running in Windows Azure](images/notification-app-server-portal-running-in-win.png?raw=true)

	_Notification App Server portal running in Windows Azure_

	>**Note:**
	>
	>1. Ensure to replace the \<notificationappserver\> with the dns prefix of your Windows Azure deployment.
	>2. If your site still does not load return to http://windows.azure.com and ensure that your hosted service deployment is complete in the ready state.  As depicted below:
	>
	>![Deployment in Ready State](images/deployment-in-ready-state.png?raw=true)

<a name="Ex3Task2" />
#### Task 2 – Running the Notification enabled Windows Metro Style App ####

In this task, you will run the client application you created in the previous exercise to create a channel for the WNS and register it with the Notification App Server.

1.	In **Visual Studio 11** return to your Windows Metro Style App and press F5.
1.	When the application launches press the _Request and Register Channel_ button. This will request a channel from WNS and submit it to the Notifications App Server you deployed to Windows Azure.  In the TextBlock you will see that the Channel URI was sent successfully to your service.

	![Client output after successful channel request from WNS and registering with notification app server](images/client-output-after-successful-channel-requescsharp.png?raw=true)

	_Client output after successful channel request from WNS and registering with notification app server_

<a name="Ex3Task3" />
#### Task 3 – Sending Push Notifications using the ASP .NET MVC 3 Portal ####

Now that a channel has been successfully requested from WNS and registered with your Notification App Server we can now start to send notifications through the portal.

1.	Switch to the Web browser and log into your deployed application (e.g: http://**[YOUR_SUBDOMAIN]**/.cloudapp.net), using the following credentials:
	1. User Name: **admin**
	1. Password: ![password](images/password.png?raw=true) (with a zero)

1.	Once logged in, additional menu options are displayed to allow you to send push notifications and manage the images (blobs) used when sending notifications.

	![Notification App Server Home Page](images/notification-app-server-home-page.png?raw=true)

	_Notification App Server Home Page_

1.	Click the **Push Notifications** menu option. Here you will see the channel that you requested using the sample Metro app that registered with your Web Role.

	![Pushing Notifications](images/pushing-notifications.png?raw=true)
	
	_Pushing Notifications_

	>**Note:** If you re-register your channel from your client app while this page is open it is worthwhile refreshing the page to ensure that you have captured the latest channel uri.

1.	You can now send your first toast notification to this channel. Click on the **Send Notification** button to open the notifications template dialog window.
1.	 Select **Toast** in the first drop-down list and then select the **ToastImageAndText01** template.
1.	Configure the template column with your Square **WindowsAzureLogo.png** and type some text into **Regular text** as per below:

	![Selecting notification type and template](images/selecting-notification-type-and-template.png?raw=true)

	_Selecting notification type and template_

1.	Press **Send** and observe that the Web portal indicates that the message was successfully delivered to WNS and the Toast notification arrives to the Client application.

	![Notification sent confirmation](images/notification-sent-confirmation.png?raw=true)

	_Notification sent confirmation_

	>**Note:**  If you are getting a 403 unauthorized or no notifications showing up please try refreshing the page to ensure that the channel that you are sending to is refreshed.  Following this please check that you created your WNS credentials with the correct CN from your package.appxmanifest and that you have configured your package.appxmanifest and ServiceConfiguration.cscfg correctly.

1.	Now that we have seen how to send a Toast we will send a Tile notification. Select **Tile** in the first drop-down and then select the **TileWideImageAndText01** template.
1.	Configure the template column with:
	1.	Your wide logo **WindowsAzureLogoWide.png**.
	1.	Custom text to be displayed in **Large Text** and **Regular Text**.
	1.	Press **Send**.

		![Pushing a Tile notification](images/pushing-a-tile-notification.png?raw=true)

		_Pushing a Tile notification_

1.	Return to Start by **pressing** the Windows key ( ![start](images/start.png?raw=true)) and observe that your Tile notification has now been delivered and is being displayed.

	![Delivered Tile notification](images/delivered-tile-notification.png?raw=true)
	
	_Delivered Tile notification_

1.	Now that we have seen how to send a Tile we will send a Badge notification. Select **Badge** in the first drop-down list and then select the **Glyph** template.
1.	In the template column select the **NewMessage** option and press **Send**.

	![Pushing a Badge notification](images/pushing-a-badge-notification.png?raw=true)

	_Pushing a Badge notification_

1.	Return to Start by **pressing** the Windows key ( ![start](images/start.png?raw=true)) and observe that your Tile has now the Badge updated to see the NewMessage Glyph type:

	![Updated Tile notification with a Badge](images/updated-tile-notification-with-a-badge.png?raw=true)

	_Updated Tile notification with a Badge_

	>**Note:** This concludes the overview of how to send Toast, Tile and Badge notifications using the Windows Azure Toolkit for Windows 8. As an exercise, it’s recommended to spend some time both exploring the rich set of templates available to each of the different notification types and how you can use blob storage to store your image assets for notifications.

---

<a name="Summary" />
## Summary ##
By completing this Hands-On Lab you have learned how to:

-	 Use the Windows Azure Platform Management Portal to create storage accounts and hosted service components.
-	Use the WNS and Live Connect Portal to request credentials for use with WNS.
-	Deploy service component packages using the Windows Azure Platform Management Portal user interface.
-	Configure a Windows Metro Style client to receive notifications.
-	Test sending notifications to your client app via WNS using the Windows Azure Toolkit for Windows 8 portal.

If you would like the full codebase for the **Notification App Server** to update for your own applications please download the **Windows Azure Training Kit for Windows 8** (http://watwindows8.codeplex.com).
If you have any feedback on this lab please contact [watwindows8@microsoft.com](mailto:watwindows8@microsoft.com).