Pizza App

Table of Contents
	•	Introduction
	•	Features
	•	Tech Stack
	•	Installation & Setup
	•	1. Clone the Repository
	•	2. Install Dependencies
	•	3. Configure API Keys
	•	4. Running the Application
	•	Deployment
	•	License

Introduction

Pizza App is a mobile UI prototype developed using Xamarin.Forms. It provides a seamless ordering experience, allowing users to browse available pizzas, customize their selection, add items to the cart, proceed to checkout, and manage their order history. The app follows modern UI/UX principles, offering a clean and intuitive interface.

Features
	•	Menu Browsing: View available pizzas with images, descriptions, and pricing.
	•	Customization: Choose size, crust, and additional toppings.
	•	Cart Management: Add, remove, and update item quantities.
	•	Checkout: Secure order placement with address selection and payment options.
	•	Order History: Track past orders and reorder with a single tap.
	•	Profile Management: Update user information and preferences.
	•	Dark Mode Support: Adapts to system theme settings.

Tech Stack

Frontend:
	•	Xamarin.Forms (XAML for UI)
	•	C# (Business Logic)
	•	Shell Navigation

Backend:
	•	Mock Data Store (Expandable to Firebase/REST API)
	•	Google Maps API (For address selection)

Deployment & Dev Tools:
	•	Visual Studio
	•	Android Emulator / iOS Simulator
	•	NuGet Package Manager

Installation & Setup

1. Clone the Repository

git clone https://github.com/your-username/pizza-app.git
cd pizza-app

2. Install Dependencies

# Restore NuGet packages in Visual Studio

3. Configure API Keys

For Android, add your Google Maps API Key in AndroidManifest.xml:

<meta-data android:name="com.google.android.geo.API_KEY" android:value="YOUR_GOOGLE_MAPS_API_KEY" />

4. Running the Application
	•	Open Pizza_App.sln in Visual Studio.
	•	Select Android Emulator or iOS Simulator.
	•	Click Run (F5).

Deployment

Pizza App can be deployed to both Android and iOS platforms. The deployment process involves:
	1.	Building the Application: Use Visual Studio to generate APK/IPA files.
	2.	Testing on Devices: Ensure UI responsiveness across different screen sizes.
	3.	Publishing: Deploy to Google Play Store and Apple App Store (if required).

License

This project is licensed under the MIT License.
