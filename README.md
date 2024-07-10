# 開藥系統



***

簡述: 

該WEB軟體分醫師和藥局兩端 

- 醫師端: 由醫師從主頁上方使用員工編號登入系統, 管理患者並為患者開藥

- 藥局端: 首頁下方登入藥局端, 可從頁面下方看到所有開藥紀錄, 患者至藥局領藥後, 該患者開藥紀錄會由藥局端更新其領藥時間

***



***

使用工具及技術:

- 前端 : React 

- 後端 : ASP.NET Core Web API

- 資料庫 : Microsoft SQL Server

- IDE: Visual Studio

***



## 首頁(/) 

![](/pics/首頁_1.jpg)

[前端程式碼](/medicine.client/src/pages/LogIn.jsx)



> 新增醫師

![](/pics/首頁_2.jpg)

[對應API: /Account/NewEmployee, 後端程式碼: Account控制器第21-42列](/Medicine.Server/Controllers/AccountController.cs)


> 使用員工編號登入系統

![](/pics/首頁_3.jpg)

[對應API: /Account/DoctorSignIn, 後端程式碼: Account控制器第44-63列](/Medicine.Server/Controllers/AccountController.cs)



## 醫師端頁面(/Doctor)

![](/pics/醫師_1.jpg)

[前端程式碼](/medicine.client/src/pages/Doctor.jsx)


> 顯示醫師科別及名稱於頁面上方

![](/pics/醫師_5.jpg)

[對應API: /Doctor/{num}, 後端程式碼: Doctor控制器第18-38列](/Medicine.Server/Controllers/DoctorController.cs)


> 新增患者

![](/pics/醫師_2.jpg)

[對應API: /Doctor/{num}/AddPatient, 後端程式碼: Doctor控制器第40-61列](/Medicine.Server/Controllers/DoctorController.cs)


> 使用患者ID搜尋患者(點擊患者ID進入患者頁面)

![](/pics/醫師_3.jpg)

[對應API: /Doctor/{num}/Patient/{id}, 後端程式碼: Doctor控制器第86-107列](/Medicine.Server/Controllers/DoctorController.cs)


> 患者名單顯示於頁面最下方(點擊患者ID進入患者頁面)

![](/pics/醫師_4.jpg)

[對應API: /Doctor/{num}/Patients, 後端程式碼: Doctor控制器第63-84列](/Medicine.Server/Controllers/DoctorController.cs)



## 患者頁面(/Patient)

![](/pics/患者_1.jpg)

[前端程式碼](/medicine.client/src/pages/Patient.jsx)


> 新增開藥

![](/pics/患者_2.jpg)

[對應API: /Patient/{id}/AddRecord, 後端程式碼: Patient控制器第100-122列](/Medicine.Server/Controllers/PatientController.cs)


> 可透過記錄ID, 日期以及藥物名稱查詢開藥紀錄

![](/pics/患者_3.jpg)

[對應API: /Patient/{id1}/Record/{id2}, 後端程式碼: Patient控制器第64-98列](/Medicine.Server/Controllers/PatientController.cs)

![](/pics/患者_4.jpg)

[對應API: /Patient/{id}/RecByTime/{time}, 後端程式碼: Patient控制器第146-188列](/Medicine.Server/Controllers/PatientController.cs)

![](/pics/患者_5.jpg)

[對應API: /Patient/{id}/RecByMed/{name}, 後端程式碼: Patient控制器第190-235列](/Medicine.Server/Controllers/PatientController.cs)


> 開藥紀錄顯示於頁面最下方

![](/pics/患者_6.jpg)

[對應API: /Patient/{id}/Records, 後端程式碼: Patient控制器第19-62列](/Medicine.Server/Controllers/PatientController.cs)



## 藥局端頁面(/Pharmacy)

![](/pics/藥局_1.jpg)

[前端程式碼](/medicine.client/src/pages/Pharmacy.jsx)



> 從首頁最下方登入藥局頁面

![](/pics/藥局_3.jpg)

[對應API: /Account/LogInPharmacy, 後端程式碼: Account控制器第65-74列](/Medicine.Server/Controllers/AccountController.cs)


> 新增藥物

![](/pics/藥局_2.jpg)

[對應API: /Pharmacy/AddMedicine, 後端程式碼: Pharmacy控制器第19-41列](/Medicine.Server/Controllers/PharmacyController.cs)


> 以藥物名稱查詢藥物

![](/pics/藥局_4.jpg)

[對應API: /Pharmacy/Medicine/{name}, 後端程式碼: Pharmacy控制器第43-67列](/Medicine.Server/Controllers/PharmacyController.cs)


> 所有患者開藥紀錄顯示於頁面最下方, 分為未領(開藥名冊)和已領藥

![](/pics/藥局_5.jpg)

[對應API: /Pharmacy/Records, 後端程式碼: Pharmacy控制器第88-148列](/Medicine.Server/Controllers/PharmacyController.cs)


> 若患者欲領藥, 點擊未領紀錄最後方按鈕「患者領取」進行領藥

![](/pics/藥局_6.jpg)

[對應API: /Patient/{id}/GetMed, 後端程式碼: Patient控制器第124-144列](/Medicine.Server/Controllers/PatientController.cs)


> 患者領藥後, 紀錄會出現在已領藥名冊, 並新增領藥時間

![](/pics/藥局_7.jpg)

[對應API: /Pharmacy/Records, 後端程式碼: Pharmacy控制器第88-148列](/Medicine.Server/Controllers/PharmacyController.cs)


## API文件

> Account Controller

![](/pics/API_1.jpg)

> Doctor Controller

![](/pics/API_2.jpg)

> Patient Controller

![](/pics/API_3.jpg)

> Pharmacy Controller

![](/pics/API_4.jpg)



## 資料庫

> 概觀

![](/pics/資料庫_1.jpg)

> doctor資料表

![](/pics/資料庫_2.jpg)

欄位: 

- 員工編號(num, 主鍵)

- 員工(醫師)名稱(name)

- 科別(department)



> patient資料表

![](/pics/資料庫_3.jpg)

欄位:

- 患者ID(id, 主鍵)

- 患者名稱(name)

- 員工(醫師)編號(doctorNum, 外來鍵)



> medicine資料表

![](/pics/資料庫_4.jpg)

欄位:

- 藥物ID(id, 主鍵)

- 藥物名稱(name, 唯一值)

- 症狀(symptom)

- 藥效(effect)

- 副作用(sideEffect)

- 用量(dose)



> record資料表

![](/pics/資料庫_5.jpg)

欄位:

- 紀錄ID(id, 主鍵)

- 開藥時間(time)

- 開藥數量(quantity)

- 患者ID(patientId, 外來鍵)

- 藥物ID(medicineId, 外來鍵)

- 員工編號(docNum, 外來鍵)

- 領藥時間(getMedTime)
