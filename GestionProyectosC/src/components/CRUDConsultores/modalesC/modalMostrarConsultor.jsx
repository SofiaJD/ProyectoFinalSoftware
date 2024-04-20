import React, { useState, useEffect } from 'react';
import './modal.css';
import api from '../../../api';

const ModalMostrarConsultor = ({ idConsultor }) => {
  const [consultor, setConsultor] = useState(null);

  useEffect(() => {
    const fetchConsultorData = async () => {
      try {
        const response = await api.get(`/Consultor/${idConsultor}`);
        const ConsultorData = await response.data;

        setConsultor(ConsultorData);
      } catch (error) {
        console.error(error);
      }
    };
    if (idConsultor) {
      fetchConsultorData();
    }
  }, [idConsultor]);

  const handleCerrarModal = () => {
    const modal = document.getElementById('modalMostrarConsultor');
    modal.close();
  };

  return (
    <dialog id="modalMostrarConsultor">
      <button type="button" className="btnCerrarModal" onClick={handleCerrarModal}>
        <span className="material-symbols-outlined btnCerrar">close</span>
      </button>
      <div id="container">
        <h2>Ver datos de consultor</h2>
        <p>Visualice todos los datos del consultor</p>
        {consultor && (
          <div id="containerInfo">
            <p><strong>ID</strong> <br/> {consultor.id}</p>
            <p><strong>Nombre completo</strong> <br/> {consultor.nombre}</p>
            <p><strong>Correo electrónico</strong> <br/> {consultor.email}</p>
            <p><strong>Teléfono</strong> <br/> {consultor.telefono}</p>
            <p><strong>Especialidad</strong> <br/> {consultor.especialidad}</p>
          </div>
        )}
      </div>
    </dialog>
  );
};

export default ModalMostrarConsultor;

