import styles from './GegevensVeld.module.css'
import { useState } from 'react'

interface GegevensVeldProps {
    mode: 'login' | 'register'
    onSubmit: (username: string, email: string, password: string, confirmpassword: string) => void
    error: string | null
}

function GegevensVeld({ mode, onSubmit, error }: GegevensVeldProps) {
    const [username, setUsername] = useState('')
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [confirmpassword, setConfirmpassword] = useState('')

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault()
        onSubmit(username, email, password, confirmpassword)
    }

    return (
        <div className={styles.gegvensveld}>
            <div className={styles.card}>
                <h2 className={styles.title}>
                    {mode === 'login' ? 'Inloggen' : 'Registreren'}
                </h2>
                <form className={styles.form} onSubmit={handleSubmit}>
                    {mode === 'register' && (
                        <input className={styles.input} placeholder="Gebruikersnaam" value={username} onChange={e => setUsername(e.target.value)} />
                    )}
                    <input className={styles.input} placeholder="E-mail" value={email} onChange={e => setEmail(e.target.value)} />
                    <input className={styles.input} type="password" placeholder="Wachtwoord" value={password} onChange={e => setPassword(e.target.value)} />
                    {mode === 'register' && (
                        <input className={styles.input} type="password" placeholder="Wachtwoord herhalen" value={confirmpassword} onChange={e => setConfirmpassword(e.target.value)} />
                    )}
                    <button className={styles.button} type="submit" disabled={mode === 'register' ? !username || !email || !password || !confirmpassword : !email || !password}>{mode === 'login' ? 'Inloggen' : 'Registreren'}
                    </button>
                    {error && <p className={styles.error}>{error}</p>}
                </form>
            </div>
        </div>
    )
}

export default GegevensVeld