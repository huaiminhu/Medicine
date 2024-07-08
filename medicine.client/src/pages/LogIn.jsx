import { useRef } from 'react';
import { useNavigate } from 'react-router-dom';

const LogIn = () => {

    const numInput = useRef(null); //登入系統處輸入員工(醫師)編號
    const nameInput = useRef(null); //新增員工處輸入醫師名稱
    const departmentInput = useRef(null); //新增員工處輸入醫師部門
    const docInput = useRef(null); //新增員工處輸入員工編號
    const emailInput = useRef(null); //藥局端登入帳號
    const passwordInput = useRef(null); //藥局端登入密碼

    //新增員工(醫師)
    const addDoctor = () => {
        fetch("https://localhost:7210/Account/NewEmployee", {
            method: "POST", 
            mode: "cors", 
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                num: numInput.current.value,
                name: nameInput.current.value,
                department: departmentInput.current.value
            })
        }).then((r) => r.status === 200 ? alert("新增成功") : alert("新增失敗"))
    }

    const navigate = useNavigate();
    //醫師登入, 並傳遞員工編號docInput.current.value
    const doctorSignIn = () => {

        fetch("https://localhost:7210/Account/DoctorSignIn", {
            method: "POST",
            mode: "cors", 
            headers: {
                "Content-Type": "application/json",
            },
            body: docInput.current.value
        }).then((r) => r.status == 200 ? navigate("/Doctor", { state: docInput.current.value }) : alert("登入失敗!"))
    }

    //藥局端登入, 若無傳遞字串state:"EmployeeLoggedIn", 將導向頁面/Failed
    const signInPharmacy = () => {

        fetch("https://localhost:7210/Account/LogInPharmacy", {
            method: "POST",
            mode: "cors",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                email: emailInput.current.value, 
                password: passwordInput.current.value
            })
        }).then(r => r.status == 200 ? navigate("/Pharmacy", { state:"EmployeeLoggedIn"}) : alert("登入失敗!"))
    }

    return (
        <div>
            <div style={{ border: '1px solid forestgreen' }}>
                <h2 style={{ backgroundColor: "lightyellow" }}>開藥前請使用員工編號登入</h2>
                <br />
                <input ref={docInput} type="password" placeholder="請輸入員工編號" />
                <button onClick={() => doctorSignIn()}>登入</button>
            </div>
            <br />
            <div style={{ border: '1px solid forestgreen' }}>
                <h3>新員工:</h3>
                <br />
                <label>員工姓名: <input ref={nameInput} type="text" placeholder="姓名" /></label>
                <br />
                <label>員工科別: <input ref={departmentInput} type="text" placeholder="科別" /></label>
                <br />
                <label>員工代碼: <input ref={numInput} type="password" placeholder="代碼" /></label>
                <br />
                <button onClick={()=>addDoctor()}>新增</button>
            </div>
            <br />
            <div style={{ border: '1px solid forestgreen' }}>
                <h2 style={{ backgroundColor: "lightcyan" }}>藥局員工請由下方登入:</h2>
                <br />
                <label>藥局Email: <input ref={emailInput} type="text" placeholder="Email" /></label>
                <br />
                <label>藥局密碼: <input ref={passwordInput} type="password" placeholder="密碼" /></label>
                <br />
                <button onClick={() => signInPharmacy()}>登入藥局</button>
            </div>
        </div>
  );
}

export default LogIn;