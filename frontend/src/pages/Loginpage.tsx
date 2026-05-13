import Navbar from "../components/Navbar"
import GegevensVeld from "../components/GegevensVeld"
import { login } from '../services/AuthService'
import { useState } from 'react'
import { useNavigate } from "react-router-dom"

function Loginpage() {
    const [error, setError] = useState<string | null>(null)
    const navigate = useNavigate()
    const handleLogin = async (username: string, email: string, password: string) => {

        const response = await login(email, password)
        if (response.ok) {
            const data = await response.json()
            localStorage.setItem('token', data.data.accessToken)
            window.location.href = '/home'
        } else {
            response.json().then(data => setError(data.message || 'Inloggen is mislukt'));
        }

    }


    return (
        <>
            <Navbar />

            <GegevensVeld mode="login" onSubmit={handleLogin} error={error} />
        </>
    
    )
} export default Loginpage