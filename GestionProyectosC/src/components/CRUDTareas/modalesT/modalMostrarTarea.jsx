import React, { useState, useEffect } from 'react';
import './modal.css';
import api from '../../../api';

const ModalMostrarTarea = ({ idTarea }) => {
  const [tarea, setTarea] = useState(null);

  useEffect(() => {
    const fetchTareaData = async () => {
      try {
        const response = await api.get(`/Tareas/${idTarea}`);
        const tareaData = await response.data;

        setTarea(tareaData);
      } catch (error) {
        console.error(error);
      }
    };
    if (idTarea) {
      fetchTareaData();
    }
  }, [idTarea]);

  const handleCerrarModal = () => {
    const modal = document.getElementById('modalMostrarTarea');
    modal.close();
  };

  return (
    <dialog id="modalMostrarTarea">
      <button type="button" className="btnCerrarModal" onClick={handleCerrarModal}>
        <span className="material-symbols-outlined btnCerrar">close</span>
      </button>
      <div id="container">
        <h2>Ver datos de tarea</h2>
        <p>Visualice todos los datos del tarea</p>
        {tarea && (
          <div id="containerInfo">
            <p><strong>ID</strong> <br/> {tarea.id}</p>
            <p><strong>Nombre</strong> <br/> {tarea.nombre}</p>
            <p><strong>Descripción</strong> <br/> {tarea.descripcion}</p>
            <p><strong>Fecha de inicio</strong> <br/> {tarea.fechaInicio}</p>
            <p><strong>Fecha de finalización</strong> <br/> {tarea.fechaFin}</p>
            <p><strong>Estado</strong> <br/> {tarea.estado}</p>
            <p><strong>Proyecto</strong> <br/> {tarea.proyectoNombre}</p>
          </div>
        )}
      </div>
    </dialog>
  );
};

export default ModalMostrarTarea;

