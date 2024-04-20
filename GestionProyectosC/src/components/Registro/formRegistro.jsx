import React, { useState }  from 'react';
import { Link } from 'react-router-dom'
import FiguraDecorativa from '../../assets/Figura decorativa.svg'
import './registro.css'
import api from '../../api';


const formRegistro = () => {

    const [nombre, setNombre] = useState('');
    const [apellido, setApellido] = useState('');
    const [nombreUsuario, setNombreUsuario] = useState('');
    const [telefono, setTelefono] = useState('');
    const [rolUsuario, setRolUsuario] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [confirmPassword, setConfirmPassword] = useState('');
    const [registroExitoso, setRegistroExitoso] = useState(false);

    const handleSubmit = async (e) => {
        e.preventDefault();
    
        const data = {
          nombre,
          apellido,
          nombreUsuario,
          telefono,
          rolUsuario,
          email,
          password,
          confirmPassword
        };
    
        try {
          const response = await api.post('/Usuarios/registro', data);
          console.log(response);
          setRegistroExitoso(true);
        } catch (error) {
          console.error(error);
        }
      };

  return (
    <div id="registroContainer">        
        <div id="formContainer" >
            <div id="info">
                <h1>¡Únete a nosotros!</h1>
                <p>Completa el formulario para crear tu cuenta.</p>
            </div>
            <div id="form">
                <h2>Registrarse</h2>
                <form action="" id="registro" onSubmit={handleSubmit}>
                    <div>
                        <label htmlFor="nombre">
                            Nombre
                            <br></br>
                            <input type="text" id="nombre" placeholder="John" 
                            value={nombre} onChange={(e) => setNombre(e.target.value)} required></input>
                        </label>
                        <label htmlFor="apellido">
                            Apellido
                            <br></br>
                            <input type="text" id="apellido" placeholder="Doe" 
                            value={apellido} onChange={(e) => setApellido(e.target.value)} required></input>
                        </label>
                    </div>
                    <div>
                        <label htmlFor="usuario">
                            Usuario
                            <br></br>
                            <input type="text" id="usuario" placeholder="John_Doe" 
                            value={nombreUsuario} onChange={(e) => setNombreUsuario(e.target.value)} required></input>
                        </label>
                        <label htmlFor="telefono">
                            Teléfono
                            <br></br>
                            <input type="tel" id="telefono" placeholder="8090000000" 
                            value={telefono} onChange={(e) => setTelefono(e.target.value)} required></input>
                        </label>
                    </div>
                    <div>
                        <label htmlFor="rol">
                            Rol
                            <br></br>
                            <input type="text" id="rol" placeholder="Usuario" 
                            value={rolUsuario} onChange={(e) => setRolUsuario(e.target.value)} required></input>
                        </label>
                        <label htmlFor="correoElectronico">
                            Correo electrónico
                            <br></br>
                            <input type="email" id="correoElectronico" placeholder="ejemplo@ejemplo.com" 
                            value={email} onChange={(e) => setEmail(e.target.value)} required></input>
                        </label>
                    </div>
                    <div>
                        <label htmlFor="contrasena">
                            Contraseña
                            <br></br>
                            <input type="password" id="contrasena" placeholder="**********" 
                            value={password} onChange={(e) => setPassword(e.target.value)} required></input>
                        </label>
                        <label htmlFor="confirmarContrasena">
                            Confirmar contraseña
                            <br></br>
                            <input type="password" id="confirmarContrasena" placeholder="**********" 
                            value={confirmPassword} onChange={(e) => setConfirmPassword(e.target.value)} required></input>
                        </label>
                    </div>
                    {registroExitoso && (
                        <div id="successMessage">
                            <p>¡Registro exitoso! Te hemos redirigido al inicio de sesión.</p>
                            {setTimeout(() => (window.location.href = "/Login"), 3000)}
                        </div>
                    )}
                </form>
                <button type="submit" form="registro">Crear cuenta</button>
                <p>¿Ya tienes una cuenta? <Link to="/Login">Inicia sesión</Link></p>
            </div>
        </div>
        <img id="figuraDecorativa" src={FiguraDecorativa}></img>
    </div>
  );
};

export default formRegistro;

