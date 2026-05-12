import styles from './GegevensVeld.module.css'
interface GegevensVeldProps {
    mode: 'login' | 'register'
}

function GegevensVeld({ mode }: GegevensVeldProps) {
    return (
        <div className={styles.gegvensveld}>
            <div className={styles.card}>
                <h2 className={styles.title}>
                    {mode === 'login' ? 'Inloggen' : 'Registreren'}
                </h2>
                <form className={styles.form}>
                    {mode === 'register' && (
                        <input className={styles.input} placeholder="Gebruikersnaam" />
                    )}
                    <input className={styles.input} type="email" placeholder="E-mail" />
                    <input className={styles.input} type="password" minLength={6} placeholder="Wachtwoord" />
                    {mode === 'register' && (
                        <input className={styles.input} type="password" minLength={6} placeholder="Wachtwoord herhalen" />
                    )}
                    <button className={styles.button} type="submit">
                        {mode === 'login' ? 'Inloggen' : 'Registreren'}
                    </button>
                </form>
            </div>
        </div>
    )
}

export default GegevensVeld