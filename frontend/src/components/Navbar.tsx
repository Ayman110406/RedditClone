import styles from './Navbar.module.css'
import { NavLink } from 'react-router-dom'
import { useState } from 'react'


function Navbar() {
    const [token, setToken] = useState(localStorage.getItem('token'))


    return (
        <>

            <nav className={styles.navbar}>
                <NavLink to={token ? '/home' : '/'}  className={styles.logoLink}>
                    <p className={styles.logo}>Reddit Clone</p>
                </NavLink>
                <div className={styles.buttons}>
                    {token ? (
                        <button className={styles.button} onClick={() => {
                            localStorage.removeItem('token')
                            setToken(null)
                            window.location.href = '/'
                        }}>Uitloggen</button>
                    ) : (
                        <>
                            <NavLink to="/register"><button className={styles.button}>Registreren</button></NavLink>
                            <NavLink to="/login"><button className={styles.button}>Inloggen</button></NavLink>
                        </>
                    )}
                </div>
            </nav>

        </>
    )
}

export default Navbar
