import styles from './Navbar.module.css'
import { NavLink } from 'react-router-dom'


function Navbar() {

    return (
        <>

            <nav className={styles.navbar}>
                <NavLink to="/" className={styles.logoLink}>
                    <p className={styles.logo}>Reddit Clone</p>
                </NavLink>
                <div className={styles.buttons}>
                    <NavLink to="/register" className={styles.button}>Registreren</NavLink>
                    <NavLink to="/login" className={styles.button}>Inloggen</NavLink>
                </div>
            </nav>

        </>
    )
}

export default Navbar
