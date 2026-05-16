import logo from '../images/logo.svg'
import styles from './Startpage.module.css'
function Startpage() {

    return (
        <>
           
            <div className={styles.container}>
                <h1 className={styles.title}>Welkom bij Reddit Clone</h1>
                <img src={logo} alt="Reddit Clone logo" className={styles.logo} />
            </div>
        </>
    )
}

export default Startpage
