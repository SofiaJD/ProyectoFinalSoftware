import Navbar from '../components/Navbar1/navbar';
import './App.css'
import ImagenDecorativa from '../assets/ImagenInicio.png'

function App() {

  return (
    <>
    <header>
      <Navbar></Navbar> 
      <div id="banner">
        <div id="bienvenida">
          <h1>Gestión PHC</h1>
          <p>Optimizando Proyectos, Recursos Humanos y Consultoría</p>
        </div>
        <div id="imagenDecorativa">
          <img src={ImagenDecorativa} width={"600px"} />
        </div>
      </div>
    </header> 
      
    </>
  )
}

export default App

