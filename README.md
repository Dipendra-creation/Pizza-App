# Pizza App - Xamarin.Forms Mobile UI Prototype

## Table of Contents
- [Introduction](#introduction)
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Installation & Setup](#installation--setup)
  - [1. Clone the Repository](#1-clone-the-repository)
  - [2. Install Dependencies](#2-install-dependencies)
  - [3. Configure API Keys](#3-configure-api-keys)
  - [4. Running the Application](#4-running-the-application)
- [Deployment](#deployment)
- [License](#license)

---

## Introduction
Pizza App is a mobile application built using **Xamarin.Forms** that allows users to browse different types of pizzas, customize their orders, add items to the cart, place orders, and track their order history. The application provides a modern and intuitive user interface, ensuring a seamless user experience.

---

## Features
- **Browse Menu:** View available pizzas with images, descriptions, and prices.
- **Customization:** Choose pizza size, crust type, and additional toppings.
- **Cart Management:** Add, remove, and update items in the cart.
- **Checkout Process:** Enter delivery details, select a payment method, and place an order.
- **Order History:** View past orders and reorder quickly.
- **Profile Management:** Update user details and preferences.
- **Dark Mode Support:** Adjusts based on system theme settings.

---

## Tech Stack
### **Frontend:**
- **Xamarin.Forms (XAML for UI)**
- **C# (Business Logic)**
- **Shell Navigation**

### **Backend:**
- **Mock Data Store (Expandable to Firebase or a REST API)**
- **Google Maps API (For address selection)**

### **Deployment & Dev Tools:**
- **Visual Studio (For Development)**
- **Android Emulator / iOS Simulator (For Testing)**
- **NuGet Package Manager (For Dependencies)**

---

## Installation & Setup

### **1. Clone the Repository**
```sh
git clone https://github.com/your-username/pizza-app.git
cd pizza-app
```

### **2. Install Dependencies**
```sh
# Open the solution file in Visual Studio and restore NuGet packages.
```

### **3. Configure API Keys**
For Android, add your Google Maps API Key in `AndroidManifest.xml`:
```xml
<meta-data android:name="com.google.android.geo.API_KEY" android:value="YOUR_GOOGLE_MAPS_API_KEY" />
```

### **4. Running the Application**
- Open `Pizza_App.sln` in Visual Studio.
- Select Android Emulator or iOS Simulator.
- Click **Run (F5)**.

---

## Deployment
Pizza App can be deployed to both Android and iOS platforms. The deployment process involves:
1. **Building the Application:** Generate APK for Android and IPA for iOS using Visual Studio.
2. **Testing on Physical Devices:** Ensure smooth functionality across different screen sizes.
3. **Publishing to Stores:** Upload to Google Play Store and Apple App Store if required.

---

## License
This project is licensed under the **MIT License**.
