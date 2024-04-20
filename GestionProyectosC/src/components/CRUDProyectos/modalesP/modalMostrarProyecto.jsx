import React, { useState, useEffect } from 'react';
import './modal.css';
import api from '../../../api';

const ModalMostrarProyecto = ({ idProyecto }) => {
  const [proyecto, setProyecto] = useState(null);
  const [asignacion, setAsignacion] = useState(null);

  useEffect(() => {
    const fetchProyectoData = async () => {
      try {
        const response = await api.get(`/Proyecto/${idProyecto}`);
        const proyectoData = await response.data;

        setProyecto(proyectoData);
      } catch (error) {
        console.error(error);
      }
    };
    if (idProyecto) {
      fetchProyectoData();
    }
  }, [idProyecto]);

  

  const handleCerrarModal = () => {
    const modal = document.getElementById('modalMostrarProyecto');
    modal.close();
  };

  return (
    <dialog id="modalMostrarProyecto">
      <button type="button" className="btnCerrarModal" onClick={handleCerrarModal}>
        <span className="material-symbols-outlined btnCerrar">close</span>
      </button>
      <div id="container">
        <h2>Ver datos de proyecto</h2>
        <p>Visualice todos los datos del proyecto</p>
        {proyecto && (
          <div id="containerInfo">
            <p><strong>ID</strong> <br/> {proyecto.id}</p>
            <p><strong>Nombre</strong> <br/> {proyecto.nombre}</p>
            <p><strong>Descripción</strong> <br/> {proyecto.descripcion}</p>
            <p><strong>Fecha de inicio</strong> <br/> {proyecto.fechaInicio}</p>
            <p><strong>Fecha de finalización</strong> <br/> {proyecto.fechaFin}</p>
            <p><strong>Estado</strong> <br/> {proyecto.estado}</p>
            <p><strong>Cliente</strong> <br/> {proyecto.nombreCliente}</p>
          </div>
        )}
      </div>
    </dialog>
  );
};

export default ModalMostrarProyecto;


