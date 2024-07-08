import { useLocation, Link } from "react-router-dom";
import { useState, useEffect, useRef } from 'react'

const Doctor = () => {

    const location = useLocation();
    const doctorNum = location.state; //從前主頁面(/)傳遞的醫生員工編號
    
    const [docInfo, setDocInfo] = useState({name:null, department:null})
    //顯示醫師科別及名稱於頁面上方
    useEffect(() => {
        fetch("https://localhost:7210/Doctor/" + doctorNum).then(r => r.json()).then(d => setDocInfo(d))
    }, [])

    const patientName = useRef(null)
    
    //新增患者
    const addPatient = () => {
        
        fetch("https://localhost:7210/Doctor/" + doctorNum + "/AddPatient", {
            method: "POST",
            mode: "cors", 
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                name: patientName.current.value,
                doctorNum: doctorNum
            })
        }).then(r => r.status == 200 ? alert("新增成功") : alert("新增失敗"))
        
    }
    
    //查詢患者並顯示於頁面
    const searchId = useRef(null)
    const [search, setSearch] = useState({id:null, name:null, num:null})
    const searchPatient = () => {
        fetch("https://localhost:7210/Doctor/" + doctorNum + "/Patient/" + searchId.current.value)
                .then(r => r.json()).then(d => setSearch(d))
    }

    //頁面下方顯示該醫生所有患者
    const [list, setList] = useState([])
    
    useEffect(() => {
        fetch("https://localhost:7210/Doctor/" + doctorNum +
            "/Patients").then(r => r.json()).then(d => setList(d))
    }, [])
    
    
    return (
      <div>
          <div>
                <h2 style={{ backgroundColor: "lightyellow" }}>{docInfo.department} {docInfo.name}</h2>
          </div>
          <br />
          <br />
            <div style={{ border: '1px solid forestgreen' }}>
              <h3>新增患者</h3>
              <input ref={patientName} type="text" placeholder="患者姓名" />
                <button onClick={() => addPatient()}>新增</button>
          </div>
          <br />
            <div style={{ border: '1px solid forestgreen' }}>
              <h3>搜尋患者</h3>
              <input ref={searchId} type="text" placeholder="患者ID" />
              <button onClick={() => searchPatient()}>查詢</button>
              <br />
              {search.id != null ? <div>
                  <table>
                      <thead>
                          <tr>
                              <th>患者ID</th>
                              <th>患者姓名</th>
                          </tr>
                      </thead>
                      <tbody>
                          <tr>
                              <td><Link to='/Patient' state={{ state: { patientId: search.id, num: doctorNum, patient_name:search.name } }}>{search.id}</Link></td>
                              <td>{search.name}</td>
                          </tr>
                      </tbody>
                  </table>
              </div>:<div></div>}
          </div>
          <br />
          <br />
            <div>
                <h2 style={{ backgroundColor: "lightcyan" }}>患者名單</h2>
              <br />
              <table>
                  <thead>
                      <tr>
                          <th>患者ID</th>
                          <th>患者姓名</th>
                      </tr>
                  </thead>
                  <tbody>
                      {list.map((data, index) => {
                          return (<tr key={index}>
                              <td><Link to='/Patient' state={{ state: { patientId: data.id, num: doctorNum, patient_name: data.name } }}>{data.id}</Link></td>
                                    <td>{data.name}</td>
                                  </tr>)
                      })}
                  </tbody>
              </table>
          </div>
      </div>
  );
}

export default Doctor;