import './App.css'
import Homepage from './pages/Homepage.tsx'
import Loginpage from './pages/Loginpage.tsx'
import Registerpage from './pages/Registerpage.tsx'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
function App() {

  return (
      <>
          <BrowserRouter>
              <Routes>
                  <Route path='/' element={<Homepage />} />
                  <Route path='/login' element={<Loginpage />} />
                  <Route path='/register' element={<Registerpage />} />
              </Routes>
          </BrowserRouter> 
    </>
  )
}

export default App
