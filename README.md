# 📦 Inventory Management System (IMS)

> A robust, full-stack inventory tracking application built with ASP.NET Core MVC. Features real-time stock monitoring, role-based access control, and a "Live Store" point-of-sale interface.

## 📖 About the Project

This Inventory Management System is designed to streamline retail operations. It allows businesses to manage products, suppliers, and customers while tracking stock levels in real-time. The core highlight is the **Live Store** module, which prevents overselling by validating stock quantities instantly during checkout and automatically recording sales history with historical pricing.

## 🚀 Key Features

* **🛒 Live Store (POS):**
    * Real-time stock validation (prevents selling more items than available).
    * Automatic total price calculation (`Unit Price` * `Quantity`).
    * Instant stock deduction upon purchase.
* **📊 Dynamic Dashboard:**
    * Visual indicators for Stock Levels (High, Medium, Low).
    * Role-Based Navigation (Admin, Manager, User).
* **📦 Inventory Control:**
    * Full CRUD (Create, Read, Update, Delete) for Products.
    * Categorization of items.
* **👥 Entity Management:**
    * Manage **Suppliers** and **Customers**.
    * Track **Purchases** (Restocking) and **Sales** (Outgoing).
* **💰 Financial Tracking:**
    * Records the exact **Sold Price** at the time of transaction (preserving history even if prices change later).

## 🛠️ Tech Stack

* **Framework:** ASP.NET Core 8.0 MVC
* **Language:** C#
* **Database:** SQL Server (Entity Framework Core)
* **Frontend:** Bootstrap 5, HTML5, CSS3, Razor Views
* **Tools:** Visual Studio 2026, Docker (Ready for deployment)

## 📸 Screenshots

### 🖥️ Dashboard & Products
<img width="1533" height="329" alt="image" src="https://github.com/user-attachments/assets/93ca236b-328a-4ff3-8eb8-4a2bb5acf366" />

### 🏪 Live Store Catalog
<img width="909" height="566" alt="image" src="https://github.com/user-attachments/assets/02a99242-b0a3-470d-8027-9d1ee0e0e298" />

### 🛒 Checkout & Live Validation
<img width="732" height="584" alt="image" src="https://github.com/user-attachments/assets/7538aa18-42b1-4252-9231-d4b322d7b788" />

## ⚙️ Local Installation & Setup

Follow these steps to run the project on your local machine:

1.  **Clone the Repository**
    ```bash
    git clone [https://github.com/alyankabir17/InventorySystem.git](https://github.com/alyankabir17/InventorySystem.git)
    ```

2.  **Configure Database**
    * Open `appsettings.json`.
    * Ensure the `DefaultConnection` string points to your local SQL Server instance.

3.  **Run Migrations**
    Open the **Package Manager Console** in Visual Studio and run:
    ```powershell
    add-migration initialCreate
    Update-Database
    ```
    *This will create the database and all required tables (Products, Sales, Users, etc.) automatically.*

4.  **Run the Application**
    * Press `F5` or click **Run** in Visual Studio.
    * Navigate to `https://localhost:7140` (or your specific port).

## 🐳 Deployment (Docker/Railway)

The project includes a `Dockerfile` for easy deployment to cloud platforms like Railway or Azure.

1.  Push code to GitHub.
2.  Connect repository to Railway.
3.  Set the `ConnectionStrings__DefaultConnection` environment variable to your cloud database provider.

## 🤝 Contributing

1.  Fork the Project
2.  Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3.  Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4.  Push to the Branch (`git push origin feature/AmazingFeature`)
5.  Open a Pull Request

## 📝 License

Distributed under the MIT License.
*Developed by [Alyan Kabir]*

