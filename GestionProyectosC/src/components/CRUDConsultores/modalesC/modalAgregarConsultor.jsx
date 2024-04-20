import React, {useState} from 'react';
import './modal.css';
import api from '../../../api';

const ModalAgregarConsultor = ({fetchConsultores}) => {

    const [nombre, setNombre] = useState('');
    const [email, setEmail] = useState('');
    const [telefono, setTelefono] = useState('');
    const [especialidad, setEspecialidad] = useState('');
    const [idsAsignaciones, setIdsAsignaciones] = useState('');

    const [registroExitoso, setRegistroExitoso] = useState(false);

    const handleAgregarConsultor = async (e) => {
        e.preventDefault();

        const data = {
        nombre,
        email,
        telefono,
        especialidad,
        };

        try {
        const response = await api.post('/Consultor', data);
        console.log(response);
        setRegistroExitoso(true);
        fetchConsultores();
        const timeoutId = setTimeout(() => {
            handleCerrarModal();
        }, 1000);
        setNombre('');
        setEmail('');
        setTelefono('');
        setEspecialidad('');
        return () => clearTimeout(timeoutId);
        } catch (error) {
        console.error(error);
        }
    };

    const handleCerrarModal = () => {
        const modal = document.getElementById('modalAgregarConsultor');
        modal.close();
      };

  return (
        <dialog id="modalAgregarConsultor">
            <button type="button" className="btnCerrarModal" onClick={handleCerrarModal}>
            <span className="material-symbols-outlined btnCerrar">
                close
            </span>
        </button>
        <div id="container">
          <h2>Agregar nuevo consultor</h2>
          <p>Complete los campos a continuación para registrar un nuevo consultor.</p>
          <form action="" id="registrarConsultor" onSubmit={handleAgregarConsultor}>
            <label htmlFor="nombre">
              Nombre completo
              <input
                type="text"
                id="nombre"
                value={nombre}
                onChange={(e) => setNombre(e.target.value)}
                required
              />
            </label>
            <label htmlFor="email">
              Correo electrónico
              <input
                type="email"
                id="email"
                value={email}
                onChange={(e) => setEmail(e.target.value)}
                required
              />
            </label>
            <label htmlFor="telefono">
              Teléfono
              <input
                type="tel"
                id="telefono"
                value={telefono}
                onChange={(e) => setTelefono(e.target.value)}
                required
              />
            </label>
            <label htmlFor="especialidad">
              Especialidad
              <input
                type="text"
                id="especialidad"
                value={especialidad}
                onChange={(e) => setEspecialidad(e.target.value)}
                required
              />
              </label>
            {registroExitoso && (
            <div id="successMessage">
              <p>¡Registro de nuevo consultor exitoso!</p>
            </div>
          )}
          </form>
          <button type="submit" form="registrarConsultor">
              Guardar
            </button>
        </div>
        </dialog>
  );
};

export default ModalAgregarConsultor;