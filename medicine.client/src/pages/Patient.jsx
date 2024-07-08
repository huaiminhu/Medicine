import { useLocation } from "react-router-dom";
import { useRef, useState, useEffect } from 'react';

const Patient = () => {

    const location = useLocation();
    const data = location.state.state; //從前醫師頁面(/Doctor)傳遞患者名稱資料
    const quantInput = useRef(null); //新增開藥處輸入藥物數量

    //輸入藥物名稱, 使用後端API找到藥物ID
    const [medId, setMedId] = useState(-1);
    const findId = (e) => {
        fetch("https://localhost:7210/Pharmacy/FindIdByName/" + e)
            .then(r => r.json()).then(d => setMedId(d))
    }

    //新增開藥
    const addRecord = () => {        

        //開藥時間
        const created_time = new Date();
        const modified_time = created_time.toISOString().slice(0, 10);

        fetch("https://localhost:7210/Patient/" + data.patientId +
            "/AddRecord", {
            method: "POST",
            mode: "cors",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify({
                Time: modified_time,
                Quant: quantInput.current.value,
                PatientId: data.patientId,
                MedicineId: medId,
                DocNum: data.num, 
                GetMedTime: ''
            })
        }).then(r => r.status == 200 ? alert("新增成功!") : alert("新增失敗!"))
    }

    const searchId = useRef(null);
    const searchTime = useRef(null);
    const searchMed = useRef(null);
    const [result, setResult] = useState({
        id: null, 
        time: null, 
        quant: null, 
        patient: null, 
        medicine: null,
        doctor: null
    })
    const [results, setResults] = useState([])

    //使用紀錄ID尋找紀錄資料
    const searchById = () => {
        fetch("https://localhost:7210/Patient/" + data.patientId +
            "/Record/" + searchId.current.value).then(r => r.json())
            .then(d => setResult(d))
    }

    //使用開藥時間尋找紀錄資料
    const searchByTime = () => {
        fetch("https://localhost:7210/Patient/" + data.patientId + "/RecByTime/" +
            searchTime.current.value).then(r => r.json()).then(d => setResults(d))
    }

    //使用藥物名稱尋找紀錄資料
    const searchByMed = () => {
        fetch("https://localhost:7210/Patient/" + data.patientId + "/RecByMed/" +
            searchMed.current.value).then(r => r.json()).then(d => setResults(d))
    }

    //顯示該病患所有開藥資訊
    const [list, setList] = useState([])
    useEffect(() => {
        fetch("https://localhost:7210/Patient/" + data.patientId + "/Records")
            .then(r => r.json()).then(d=>setList(d))
    }, [])

    return (
        <div>
            <div>
                <h2 style={{ backgroundColor: "lightyellow" }}>{data.patient_name}</h2>
            </div>
            <br />
            <br />
            <div style={{ border: '1px solid forestgreen' }}>
                <h2 style={{ backgroundColor: "orangered" }}>新增開藥</h2>
                <br />
                <label>請輸入藥物名稱: <input onChange={e=>findId(e.target.value)} type="text" placeholder="藥物名稱" /></label>
                <br />
                <label>請輸入開藥數量: <input ref={quantInput} type="text" placeholder="藥物量" /></label>
                <br />
                <button onClick={() => addRecord()}>新增</button>
            </div>
            <br />
            <div style={{ border: '1px solid forestgreen' }}>
                <h3>查詢開藥紀錄</h3><h4>(以下擇一方式查詢)</h4>
                <label>使用紀錄ID查詢: <input ref={searchId} type="text" placeholder="請輸入紀錄ID" /></label>
                <button onClick={() => searchById()}>查詢</button>
                <br />
                {result.id != null ? <div>
                    <table>
                        <thead>
                            <tr>
                                <td>紀錄ID</td>
                                <td>藥物名稱</td>
                                <td>數量</td>
                                <td>開藥時間</td>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>{result.id}</td>
                                <td>{result.medicineName}</td>
                                <td>{result.quant}</td>
                                <td>{result.time}</td>
                            </tr>
                        </tbody>
                    </table>
                </div> : <div></div>}
                <br />
                <label>使用開藥時間查詢: <input ref={searchTime} type="text" placeholder="請輸入西元年分-月-日(例:2024-01-01)" /></label>
                <button onClick={() => searchByTime()}>查詢</button>
                <br />
                <label>使用藥物名稱查詢: <input ref={searchMed} type="text" placeholder="請輸入藥物名稱" /></label>
                <button onClick={() => searchByMed()}>查詢</button>
                <br />
                {results.length != 0 ?
                    <div>
                        <table>
                            <thead>
                                <tr>
                                    <td>紀錄ID</td>
                                    <td>藥物名稱</td>
                                    <td>數量</td>
                                    <td>開藥時間</td>
                                </tr>
                            </thead>
                            <tbody>
                                {results.map((record, index) => {
                                    return (
                                        <tr key={index}>
                                            <td>{record.id}</td>
                                            <td>{record.medicineName}</td>
                                            <td>{record.quant}</td>
                                            <td>{record.time}</td>
                                        </tr>
                                    )
                                })}
                            </tbody>
                        </table>
                    </div> : <div></div>}
            </div>
            {list.length != 0 ?
                <div>
                    <h3 style={{ backgroundColor: "lightcyan" }}>開藥紀錄</h3>
                    <br />
                    <table>
                        <thead>
                            <tr>
                                <td>紀錄ID</td>
                                <td>藥物名稱</td>
                                <td>數量</td>
                                <td>開藥時間</td>
                            </tr>
                        </thead>
                        <tbody>
                            {list.map((record, index) => {
                                return (
                                    <tr key={index}>
                                        <td>{record.id}</td>
                                        <td>{record.medicineName}</td>
                                        <td>{record.quant}</td>
                                        <td>{record.time}</td>
                                    </tr>
                                )
                            })}
                        </tbody>
                    </table>
                </div>:<div></div>}
        </div>
  );
}

export default Patient;