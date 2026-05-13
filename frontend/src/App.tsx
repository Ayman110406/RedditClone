import './App.css'
import Homepage from './pages/Homepage.tsx'
import Loginpage from './pages/Loginpage.tsx'
import Registerpage from './pages/Registerpage.tsx'
import Startpage from './pages/Startpage.tsx'
import ProtectedRoute from './components/ProtectedRoute.tsx'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
function App() {

  return (
      <>
          <BrowserRouter>
              <Routes>
                  <Route path='/' element={<Startpage />} />
                  <Route path='/login' element={<Loginpage />} />
                  <Route path='/register' element={<Registerpage />} />
                  <Route path='/home' element={
                      <ProtectedRoute>
                          <Homepage />
                      </ProtectedRoute>
                  } />
              </Routes>
          </BrowserRouter> 
    </>
  )
}

export default App
