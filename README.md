# Skillora Academy

A modern ASP.NET Core MVC web application for managing an academic system including students, instructors, courses, and departments with authentication, authorization, and email notification system.

---

## 🚀 Features

- 👨‍🎓 Student Management System  
- 👨‍🏫 Instructor Management System  
- 📚 Course Management  
- 🏢 Department Management  
- 🔐 Authentication (Login / Register / Logout)  
- 👮 Role-based Authorization (Admin / Instructor / Student)  
- ⏳ Admin Approval System for new users  
- 📧 Email Notifications using Gmail SMTP  
- ✅ Approve / Reject users from system or email links  
- 🎨 Modern responsive UI using Bootstrap  
- 🧭 Sidebar navigation based on roles  
- 🏠 Role-based Home / Dashboard pages  

---

## 👥 User Roles

### 🔴 Admin
- Approve or reject new users  
- Manage departments  
- Manage instructors  
- View all users  

### 🟣 Instructor
- Manage courses  
- View assigned students  

### 🔵 Student
- View available courses  
- Access dashboard  

---

## 📧 Email System

The system uses Gmail SMTP to send emails automatically:

### ✉️ Email Features:
- New registration notification sent to Admin  
- Account approval email sent to user  
- Account rejection email sent to user  
- Approve / Reject directly from email links  

---

## ⚙️ Email Configuration (appsettings.json)

"EmailSettings": {
  "FromEmail": "sarasamir9202@gmail.com",
  "Password": "your-app-password",
  "Host": "smtp.gmail.com",
  "Port": 587,
  "AdminEmail": "admin@gmail.com"
}

---

## 🛠️ Technologies Used

- ASP.NET Core MVC  
- Entity Framework Core  
- SQL Server  
- LINQ  
- Bootstrap 5  
- Dependency Injection  
- Gmail SMTP  

---

## 📂 Project Structure

Controllers  
Models  
Views  
Services  
Middleware  
Repositories  

---

## 🔐 Authentication Flow

1. User registers in system  
2. Admin receives email notification  
3. Admin approves or rejects user  
4. Approved users can login  
5. Role-based dashboard is shown  

---

## 📧 Admin Approval System

Admins can:
- Approve users from dashboard  
- Reject users  
- Or directly use email links to approve/reject  

---

## 🧑‍💻 How to Run Project

Clone repository:
git clone https://github.com/your-username/Skillora-Academy.git

Open project in Visual Studio  

Update appsettings.json:
- Connection String  
- Email Settings  

Run migrations (if needed)

Start project:
Ctrl + F5  

---

## 📸 Screenshots
-Home Page
<img width="1885" height="896" alt="image" src="https://github.com/user-attachments/assets/8e68edb9-bede-4a08-898f-6e0c6be99edf" />

- Login Page
<img width="1883" height="900" alt="image" src="https://github.com/user-attachments/assets/b47ba6d7-4c37-451d-9561-126d6ddc36aa" />

- Admin Dashboard
<img width="1885" height="886" alt="image" src="https://github.com/user-attachments/assets/be0ede57-b06b-4894-93e9-6424812fb1f5" />

- Instructor Dashboard
<img width="1887" height="900" alt="image" src="https://github.com/user-attachments/assets/a9c5a3e2-8db6-493a-9dd7-3927119037ca" />

 
- Pending Users Page
<img width="1907" height="900" alt="image" src="https://github.com/user-attachments/assets/2ebe55de-7f79-425e-b279-1a16e1c263f5" />

- All Students
<img width="1882" height="902" alt="image" src="https://github.com/user-attachments/assets/0472600c-7a64-4dc2-8ea3-3aca65749c88" />

- All Courses
<img width="1882" height="900" alt="image" src="https://github.com/user-attachments/assets/6594a9a0-4523-49a2-bbb4-4eeda15a7324" />

- All Departments
<img width="1896" height="898" alt="image" src="https://github.com/user-attachments/assets/09b4365f-27a0-4a5b-bd70-eb139a4744cf" />

- All Instructors
<img width="1885" height="895" alt="image" src="https://github.com/user-attachments/assets/74c1e3b6-5c2d-42e2-a0a8-ab00d91a7d28" />



 

---

## 👩‍💻 Developer

Sara Samir  
GitHub: https://github.com/Sara-Samir-9202  
LinkedIn: https://www.linkedin.com/in/sara-samir-elghazally  

---

## 💡 Notes

- Gmail App Password must be enabled  
- Admin email must be valid  
- Users cannot login until approved  

---

## ⭐ Future Improvements

- Notifications inside system  
- Chat between users  
- Reports dashboard  
- API version of project  
