import React, {useState} from 'react';
import './modalCambiarContraseña.css'
import api from '../../../api'

const modalCambiarContraseña = () => {

    const [email, setEmail] = useState('');
    const [nuevaContraseña, setNuevaContraseña] = useState('');
    const [confirmarNuevaContraseña, setConfirmarNuevaContraseña] = useState('');
    const [errorEmail, setErrorEmail] = useState('');
    const [errorPassword, setErrorPassword] = useState('');
    const [validacionEmail, setValidacionEmail] = useState(false);

    const handleCerrarModal = () => {
        const modal = document.getElementById('modalRecuperarContraseña');
        modal.close();
    };

    const validarCorreo = async (e) =>
    {
        e.preventDefault();
        try {
            const response = await api.post('/Usuarios/forgot-password', { email });
      
            if (response.status === 200) {
               
              setValidacionEmail(true);
              document.querySelector('#paso1').classList.add('oculto');
              document.querySelector('#paso2').classList.remove('oculto');
              document.querySelector('#indicadorPaso1').classList.remove('activo');
              document.querySelector('#indicadorPaso2').classList.add('activo');
            } else {
              setErrorEmail('Correo electrónico no válido.');
            }
          } catch (err) {
            console.error(err);
            setErrorEmail('Error al validar el correo electrónico.');
          }

        // document.querySelector('#paso1').classList.add('oculto');
        //     document.querySelector('#paso2').classList.remove('oculto');
        //     document.querySelector('#indicadorPaso1').classList.remove('activo');
        //     document.querySelector('#indicadorPaso2').classList.add('activo');
    }

    const cambiarContrasena = async () => {
        e.preventDefault();
    
        try {
          const response = await api.post('/Usuarios/reset-password', {
            nuevaContraseña,
            confirmarNuevaContraseña,
          });
    
          if (response.status === 200) {
            setErrorPassword('Contraseña cambiada correctamente.');
            setTimeout(() => {
              handleCerrarModal();
            }, 2000);
          } else {
            setErrorPassword('Error al cambiar la contraseña.');
          }
        } catch (err) {
          console.error(err);
          setErrorPassword('Error al cambiar la contraseña.');
        }
      };
    

  return (
    <dialog id="modalRecuperarContraseña">
         <button type="button" className="btnCerrarModal" onClick={handleCerrarModal}>
            <span className="material-symbols-outlined btnCerrar">
                close
            </span>
        </button>
        <div id="modalContenido"> 
            <h3>Recuperar contraseña</h3>
            <div id="indicadores">
                <span id="indicadorPaso1" className="indicador activo">Paso 1</span>
                <span id="indicadorPaso2" className="indicador">Paso 2</span>
            </div>
            <div id="paso1" className="paso activo">
                <p>Ingrese su correo electrónico para validar la operación.</p>
                <form action="" id="formPaso1" onSubmit={validarCorreo}>
                    <label htmlFor="email">
                        Correo electrónico
                        <input type="email" id="email" placeholder="johndoe@correo.com" value={email} onChange={(e) => setEmail(e.target.value)} required></input>
                    </label>
                </form>
                <button type="submit" id="validarEmail" form="formPaso1">Validar correo electrónico</button>
            </div>
            <div id="paso2" className="paso oculto">
                <p>Ingrese la nueva contraseña.</p>
                <form action="" id="formPaso2">
                    <label htmlFor="nuevaContraseña">
                        Nueva contraseña
                        <input type="password" id="nuevaContraseña" placeholder="*************" required></input>
                    </label>
                    <label htmlFor="confirmarNuevaContraseña">
                        Confirmar nueva contraseña
                        <input type="password" id="confirmarNuevaContraseña" placeholder="*************" required></input>
                    </label>
                </form>
                <button type="submit" id="cambiarContrasena" form="formPaso2">Cambiar contraseña</button>
            </div>
        </div>
    </dialog>
  );
};

export default modalCambiarContraseña;