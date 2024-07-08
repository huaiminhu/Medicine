import { useRef, useState, useEffect } from 'react'
import { useLocation, useNavigate } from "react-router-dom";

const Pharmacy = () => {

    const navigate = useNavigate();
    const location = useLocation();

    //若無接收到傳遞字串"EmployeeLoggedIn", 則導向頁面/Failed
    const auth = location.state;
    useEffect(() => {
        auth == "EmployeeLoggedIn" ? '' : navigate("/Failed")
    })
    const name = useRef(''); //藥物名稱
    const effect = useRef(''); //藥效
    const side = useRef(''); //副作用
    const dose = useRef(''); //使用方式

    //新增藥物
    const addMed = () => {
        fetch("https://localhost:7210/Pharmacy/AddMedicine", {
            method: "POST",
            mode: "cors",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                Name: name.current.value, 
                Effect: effect.current.value, 
                Side: side.current.value, 
                Dose: dose.current.value
            })
        }).then(r => r.status == 200 ? alert("新增成功!") : alert("新增失敗!"))
    }

    //查詢藥物資訊
    const searchMed = useRef('');
    const [result, setResult] = useState({
        Id: null, 
        Name: null, 
        Effect: null, 
        Side: null, 
        Dose: null 
    });
    const searchByMed = () => {
        fetch("https://localhost:7210/Pharmacy/Medicine/" + searchMed.current.value)
            .then(r => r.json()).then(d => setResult(d))
    }

    //顯示開藥紀錄
    const [list, setList] = useState([]);
    useEffect(() => {
        fetch("https://localhost:7210/Pharmacy/Records").then(r => r.json())
            .then(d => setList(d))
    }, [])

    //領藥
    const getMed = (id) => {

            const created_time = new Date();
            const modified_time = created_time.toISOString().slice(0, 10);
            
            fetch("https://localhost:7210/Patient/" + id + "/GetMed", {
                method: "PUT", 
                mode: "cors",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(
                    modified_time
                )
            }).then(r => r.status == 200 ? alert("已成功取藥") : alert("無法取藥"))
    }

  return (
      <div>
          <div style={{ border: '1px solid forestgreen' }}>
              <h3>新增藥物</h3>
              <label>
                  請輸入藥物名稱:
                  <input ref={name} type="text" placeholder="藥物名稱" />
              </label>
              <br />
              <label>
                  請輸入藥效:
                  <input ref={effect} type="text" placeholder="藥效" />
              </label>
              <br />
              <label>
                  請輸入藥物副作用:
                  <input ref={side} type="text" placeholder="藥物副作用" />
              </label>
              <br />
              <label>
                  請輸入服用時間及用量:
                  <input ref={dose} type="text" placeholder="(例:三餐飯後, 每次一粒)" />
              </label>
              <br />
              <button onClick={() => addMed()}>新增</button>
              <br />
              <a href="https://www.webmd.com/drugs/2/index">藥物資訊查詢(外部連結)</a>
          </div>
          <br />
          <div style={{ border: '1px solid forestgreen' }}>
              <h3>藥物查詢</h3>
              <label>請輸入藥物名稱: <input ref={searchMed} type="text" placeholder="藥物名稱" /></label>
              <button onClick={() => searchByMed()}>查詢</button>
              <br />
              {result.id != null ? <div>
                  <table>
                      <thead>
                          <tr>
                              <th>藥物名稱</th>
                              <th>藥效</th>
                              <th>副作用</th>
                              <th>服用時間及用量</th>
                          </tr>
                      </thead>
                      <tbody>
                          <tr>
                              <td>{result.name}</td>
                              <td>{result.effect}</td>
                              <td>{result.side}</td>
                              <td>{result.dose}</td>
                          </tr>
                      </tbody>
                  </table>
              </div> : <div></div>}
          </div>
          {list.length != 0 ? <div>
              <h2 style={{ backgroundColor: "orangered" }}>開藥名冊</h2>
              <table>
                  <thead>
                      <tr>
                          <td>紀錄ID</td>
                          <td>病患姓氏</td>
                          <td>藥物名稱</td>
                          <td>數量</td>
                          <td>開藥醫師</td>
                          <td>醫師科別</td>
                          <td>開藥時間</td>
                      </tr>
                  </thead>
                  <tbody>
                      {list.map((record, index) => {
                          if (record.getMedTime == '')
                            return (
                              <tr key={index}>
                                  <td>{record.id}</td>
                                  <td>{record.patientName}</td>
                                  <td>{record.medicineName}</td>
                                  <td>{record.quant}</td>
                                  <td>{record.doctorName}</td>
                                  <td>{record.department}</td>
                                  <td>{record.time}</td>
                                    <td>
                                        <button onClick={() => getMed(record.id)}>患者領取</button>
                                    </td>
                              </tr>
                              )
                      })}
                  </tbody>
              </table>
          </div> : <div></div>}
          {list.length != 0 ? <div>
              <h2 style={{ backgroundColor: "lightcyan" }}>已領藥名冊</h2>
              <table>
                  <thead>
                      <tr>
                          <td>紀錄ID</td>
                          <td>病患姓氏</td>
                          <td>藥物名稱</td>
                          <td>數量</td>
                          <td>開藥醫師</td>
                          <td>醫師科別</td>
                          <td>開藥時間</td>
                          <td>領藥時間</td>
                      </tr>
                  </thead>
                  <tbody>
                      {list.map((record, index) => {
                          if (record.getMedTime != '')
                              return (
                                  <tr key={index}>
                                      <td>{record.id}</td>
                                      <td>{record.patientName}</td>
                                      <td>{record.medicineName}</td>
                                      <td>{record.quant}</td>
                                      <td>{record.doctorName}</td>
                                      <td>{record.department}</td>
                                      <td>{record.time}</td>
                                      <td>{record.getMedTime}</td>
                                  </tr>
                              )
                      })}
                  </tbody>
              </table>
          </div> : <div></div>}
      </div>
  );
}

export default Pharmacy;