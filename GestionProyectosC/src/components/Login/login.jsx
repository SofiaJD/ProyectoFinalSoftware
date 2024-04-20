import React, { useState }  from 'react';
import { Link, Navigate } from 'react-router-dom';
import FiguraDecorativa from '../../assets/Figura decorativa.svg'
import ModalCambiarContraseña from './modalCambiarContraseña/modalCambiarContraseña';
import './login.css'
import api from '../../api';
// import PropTypes from 'prop-types'

const Login = () => {

  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [errorLogin, setErrorLogin] = useState(false);
  const [logueado, setlogueado] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();

    const data = {
      email,
      password,
    };

    try {
      const response = await api.post('/Usuarios/login', data);
      console.log(response);
      localStorage.setItem('token', response.data.token);
      setlogueado(true);
    } catch (error) {
      console.error(error);
      setErrorLogin(true);
    }
  };

  if (logueado) {
    return <Navigate to="/Inicio" />;
  }

  const handleAbrirModal = () => {
    const modal = document.querySelector('#modalRecuperarContraseña');
    modal.showModal();
  };
    
  return (
    <>
    <div id="loginContainer">        
        <div id="formContainer">
            <div id="info">
                <h1>¡Bienvenido/a!</h1>
                <p>Por favor, ingresa las credenciales para acceder a tu cuenta.</p>
            </div>
            <div id="form">
                <h2>Iniciar sesión</h2>
                <form action="" id="login" onSubmit={handleSubmit}>
                    <label htmlFor="correoElectronico">
                        Correo electrónico
                        <br></br>
                        <input type="email" id="correoElectronico" placeholder="ejemplo@ejemplo.com" 
                        value={email} onChange={(e) => setEmail(e.target.value)} required></input>
                    </label>
                    <label htmlFor="contrasena">
                        Contraseña
                        <br></br>
                        <input type="password" id="contrasena" placeholder="**********" 
                        value={password} onChange={(e) => setPassword(e.target.value)} required></input>
                        <button type="button" id="btnRecuperarContraseña" onClick={handleAbrirModal}>¿Olvidaste tu contraseña?</button>
                    </label>
                    {errorLogin && (
                      <p>Error de inicio de sesión. Revisa tus credenciales.</p>
                    )}
                </form>
                <button type="submit" form="login">Iniciar sesión</button>
                <p>¿No tienes una cuenta? <Link to="/registro">Registrate aquí</Link></p>
            </div>
        </div>
        <img id="figuraDecorativa" src={FiguraDecorativa}></img>
    </div>
    <ModalCambiarContraseña/>
    </>
  );
};

export default Login;



