# 開藥系統

前端 : React 

後端 : ASP.NET Core Web API

資料庫 : Microsoft SQL Server



## 首頁(/) 
[前端程式碼](/medicine.client/src/pages/LogIn.jsx)

![](/pics/首頁_1.jpg)




> 新增醫師

[對應API: /Account/NewEmployee, 後端程式碼: Account控制器第21-42列](/Medicine.Server/Controllers/AccountController.cs)

![](/pics/首頁_2.jpg)


> 使用員工編號登入系統

[對應API: /Account/DoctorSignIn, 後端程式碼: Account控制器第44-63列](/Medicine.Server/Controllers/AccountController.cs)

![](/pics/首頁_3.jpg)



## 醫師端頁面(/Doctor)
[前端程式碼](/medicine.client/src/pages/Doctor.jsx)

![](/pics/醫師_1.jpg)



> 顯示醫師科別及名稱於頁面上方

[對應API: /Doctor/{num}, 後端程式碼: Doctor控制器第18-38列](/Medicine.Server/Controllers/DoctorController.cs)

![](/pics/醫師_5.jpg)


> 新增患者

[對應API: /Doctor/{num}/AddPatient, 後端程式碼: Doctor控制器第40-61列](/Medicine.Server/Controllers/DoctorController.cs)

![](/pics/醫師_2.jpg)


> 使用患者ID搜尋患者(點擊患者ID進入患者頁面)

[對應API: /Doctor/{num}/Patient/{id}, 後端程式碼: Doctor控制器第86-107列](/Medicine.Server/Controllers/DoctorController.cs)

![](/pics/醫師_3.jpg)


> 患者名單顯示於頁面最下方(點擊患者ID進入患者頁面)

[對應API: /Doctor/{num}/Patients, 後端程式碼: Doctor控制器第63-84列](/Medicine.Server/Controllers/DoctorController.cs)

![](/pics/醫師_4.jpg)



## 患者頁面(/Patient)
[前端程式碼](/medicine.client/src/pages/Patient.jsx)

![](/pics/患者_1.jpg)



> 新增開藥

[對應API: /Patient/{id}/AddRecord, 後端程式碼: Patient控制器第100-122列](/Medicine.Server/Controllers/PatientController.cs)

![](/pics/患者_2.jpg)


> 可透過記錄ID, 日期以及藥物名稱查詢開藥紀錄

[對應API: /Patient/{id1}/Record/{id2}, 後端程式碼: Patient控制器第64-98列](/Medicine.Server/Controllers/PatientController.cs)

![](/pics/患者_3.jpg)

[對應API: /Patient/{id}/RecByTime/{time}, 後端程式碼: Patient控制器第146-188列](/Medicine.Server/Controllers/PatientController.cs)

![](/pics/患者_4.jpg)

[對應API: /Patient/{id}/RecByMed/{name}, 後端程式碼: Patient控制器第190-235列](/Medicine.Server/Controllers/PatientController.cs)

![](/pics/患者_5.jpg)


> 開藥紀錄顯示於頁面最下方

[對應API: /Patient/{id}/Records, 後端程式碼: Patient控制器第19-62列](/Medicine.Server/Controllers/PatientController.cs)

![](/pics/患者_6.jpg)



## 藥局端頁面(/Pharmacy)
[前端程式碼](/medicine.client/src/pages/Pharmacy.jsx)

![](/pics/藥局_1.jpg)




> 從首頁最下方登入藥局頁面

[對應API: /Account/LogInPharmacy, 後端程式碼: Account控制器第65-74列](/Medicine.Server/Controllers/AccountController.cs)

![](/pics/藥局_3.jpg)


> 新增藥物

[對應API: /Pharmacy/AddMedicine, 後端程式碼: Pharmacy控制器第19-41列](/Medicine.Server/Controllers/PharmacyController.cs)

![](/pics/藥局_2.jpg)


> 以藥物名稱查詢藥物

[對應API: /Pharmacy/Medicine/{name}, 後端程式碼: Pharmacy控制器第43-67列](/Medicine.Server/Controllers/PharmacyController.cs)

![](/pics/藥局_4.jpg)


> 所有患者開藥紀錄顯示於頁面最下方, 分為未領(開藥名冊)和已領藥

[對應API: /Pharmacy/Records, 後端程式碼: Pharmacy控制器第88-148列](/Medicine.Server/Controllers/PharmacyController.cs)

![](/pics/藥局_5.jpg)


> 若患者欲領藥, 點擊未領紀錄最後方按鈕「患者領取」進行領藥

[對應API: /Patient/{id}/GetMed, 後端程式碼: Patient控制器第124-144列](/Medicine.Server/Controllers/PatientController.cs)

![](/pics/藥局_6.jpg)


> 患者領藥後, 紀錄會出現在已領藥名冊, 並新增領藥時間

[對應API: /Pharmacy/Records, 後端程式碼: Pharmacy控制器第88-148列](/Medicine.Server/Controllers/PharmacyController.cs)

![](/pics/藥局_7.jpg)


## API

![](/pics/API_1.jpg)

![](/pics/API_2.jpg)

![](/pics/API_3.jpg)

![](/pics/API_4.jpg)
