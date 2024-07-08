import './App.css';
import LogIn from "./pages/LogIn"
import Doctor from "./pages/Doctor"
import Patient from "./pages/Patient"
import Pharmacy from "./pages/Pharmacy"
import Failed from "./pages/Failed"
import { Route, Routes } from 'react-router-dom'

function App() {
    
    return (
        <div>
            <Routes>
                <Route path="/" element={<LogIn />} />
                <Route path="/Doctor" element={<Doctor />} />
                <Route path="/Patient" element={<Patient />} />
                <Route path="/Pharmacy" element={<Pharmacy />} />
                <Route path="/Failed" element={<Failed />} />
            </Routes>
        </div>
    );
    
}

export default App;