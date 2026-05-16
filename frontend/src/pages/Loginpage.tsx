import GegevensVeld from "../components/GegevensVeld"
import { login } from '../services/AuthService'
import { useState } from 'react'

function Loginpage() {
    const [error, setError] = useState<string | null>(null)
    const handleLogin = async (_username: string, email: string, password: string) => {

        const response = await login(email, password)
        if (response.ok) {
            const data = await response.json()
            localStorage.setItem('token', data.data.accessToken)
            localStorage.setItem('username', data.data.username)
            window.location.href = '/home'
        } else {
            response.json().then(data => setError(data.message || 'Inloggen is mislukt'));
        }

    }

    return (
        <>

            <GegevensVeld mode="login" onSubmit={handleLogin} error={error} />
        </>
    
    )
} export default Loginpage