import Navbar from "../components/Navbar"
import GegevensVeld from "../components/GegevensVeld"
import { register } from '../services/AuthService'
import { useState } from 'react'
import { useNavigate } from "react-router-dom"

function Registerpage() {
    const [error, setError] = useState<string | null>(null)
    const navigate = useNavigate()
    const handleRegister = async (username: string, email: string, password: string, confirmpassword: string) => {

        if (!/^[^@\s]+@[^@\s]+\.[^@\s]+$/.test(email)) {
            setError('Ongeldig emailadres')
            return;
        }

        if (password.length < 6) {
            setError("De wachtwoord moet minimaal 6 tekens bevatten");
            return;
        }

        if (password !== confirmpassword) {
            setError("De wachtwoorden komen niet overeen");
            return;
        }

        const response = await register(username, email, password)
        if (response.ok) {
            navigate('/login');
        } else {
            response.json().then(data => {
                setError(data.message || 'Registratie mislukt')
            })
        }



    }


    return (
        <>
            <Navbar />
            <GegevensVeld mode="register" onSubmit={handleRegister} error={error} />
        </>
    )
} export default Registerpage