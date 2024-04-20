import React, { useState, useEffect } from 'react';
import './modal.css';
import api from '../../../api';

const ModalEditarConsultor = ({ idConsultor, fetchConsultores }) => {

  const [nombre, setNombre] = useState('');
  const [email, setEmail] = useState('');
  const [telefono, setTelefono] = useState('');
  const [especialidad, setEspecialidad] = useState('');
  // const [idsAsignaciones, setIdsAsignaciones] = useState('');
  const [registroExitoso, setRegistroExitoso] = useState(false);

  useEffect(() => {
    const fetchConsultorData = async () => {
      if (idConsultor) {
        try {
          const response = await api.get(`/Consultor/${idConsultor}`);
          const consultor = await response.data;

          setNombre(consultor.nombre);
          setEmail(consultor.email);
          setTelefono(consultor.telefono);
          setEspecialidad(consultor.especialidad);
        } catch (error) {
          console.error(error);
        }
      }
    };
    fetchConsultorData();
  }, [idConsultor]);

  const handleEditarConsultor = async (e) => {
    e.preventDefault();

    const data = {
      id: idConsultor,
      nombre,
      email,
      telefono,
      especialidad,
    };

    try {
      const response = await api.put('/Consultor/' + idConsultor, data);
      console.log(response);
      setRegistroExitoso(true);
      fetchConsultores();
      const timeoutId = setTimeout(() => {
        handleCerrarModal();
      }, 1000);
      return () => clearTimeout(timeoutId);
    } catch (error) {
      console.error(error);
    }
  };

  const handleCerrarModal = () => {
    const modal = document.getElementById('modalEditarConsultor');
    modal.close();
  };

  return (
    <dialog id="modalEditarConsultor">
      <button type="button" className="btnCerrarModal" onClick={handleCerrarModal}>
        <span className="material-symbols-outlined btnCerrar">close</span>
      </button>
      <div id="container">
        <h2>Editar consultor</h2>
        <p>Complete los campos a continuación para editar la información del consultor.</p>
        <form action="" id="editarConsultor" onSubmit={handleEditarConsultor}>
          <label htmlFor="id">
            ID
            <input type="text" id="id" value={idConsultor} disabled />
          </label>
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
              <p>¡Actualización de consultor exitosa!</p>
            </div>
          )}
          <button type="submit">Guardar</button>
        </form>
      </div>
    </dialog>
  );
};

export default ModalEditarConsultor;

