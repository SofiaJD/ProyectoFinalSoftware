import React, {useState} from 'react';
import './modal.css';
import api from '../../../api';

const ModalAgregarTarea = ({fetchTareas}) => {

    const [nombre, setNombre] = useState('');
    const [descripcion, setDescripcion] = useState('');
    const [fechaInicio, setFechaInicio] = useState('');
    const [fechaFin, setFechaFin] = useState('');
    const [estado, setEstado] = useState('');
    const [proyectoID, setProyectoID] = useState('');

    const [registroExitoso, setRegistroExitoso] = useState(false);

    const handleAgregarTarea = async (e) => {
        e.preventDefault();

        const data = {
        nombre,
        descripcion,
        fechaInicio,
        fechaFin,
        estado,
        proyectoID,
        };

        try {
        const response = await api.post('/Tareas', data);
        console.log(response);
        setRegistroExitoso(true);
        fetchTareas();
        const timeoutId = setTimeout(() => {
            handleCerrarModal();
        }, 1000);
        setNombre('');
        setDescripcion('');
        setFechaInicio('');
        setFechaFin('');
        setEstado('');
        setProyectoID('');
        return () => clearTimeout(timeoutId);
        } catch (error) {
        console.error(error);
        }
    };

    const handleCerrarModal = () => {
        const modal = document.getElementById('modalAgregarTarea');
        modal.close();
      };

  return (
        <dialog id="modalAgregarTarea">
            <button type="button" className="btnCerrarModal" onClick={handleCerrarModal}>
            <span className="material-symbols-outlined btnCerrar">
                close
            </span>
        </button>
        <div id="container">
          <h2>Agregar nuevo Tarea</h2>
          <p>Complete los campos a continuación para registrar una nueva Tarea.</p>
          <form action="" id="registrarTarea" onSubmit={handleAgregarTarea}>
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
            <label htmlFor="descripcion">
              Descripción
              <textarea id="descripcion" value={descripcion} onChange={(e) => setDescripcion(e.target.value)} 
              cols="30" rows="5"></textarea>
            </label>
            <label htmlFor="fechaInicio">
              Fecha de inicio
              <input
                type="date"
                id="fechaInicio"
                value={fechaInicio}
                onChange={(e) => setFechaInicio(e.target.value)}
                required
              />
            </label>
            <label htmlFor="fechaFin">
              Fecha de finalización
              <input
                type="date"
                id="fechaFin"
                value={fechaFin}
                onChange={(e) => setFechaFin(e.target.value)}
                required
              />
              </label>
              <label htmlFor="estado">
              Estado
              <input
                type="text"
                id="estado"
                value={estado}
                onChange={(e) => setEstado(e.target.value)}
                required
              />
            </label>
            <label htmlFor="proyectoID">
              Proyecto
              <input
                type="number"
                id="proyectoID"
                value={parseInt(proyectoID)}
                onChange={(e) => setProyectoID(e.target.value)}
                required
              />
            </label>
            {registroExitoso && (
            <div id="successMessage">
              <p>¡Registro de nueva tarea exitoso!</p>
            </div>
          )}
          </form>
          <button type="submit" form="registrarTarea">
              Guardar
            </button>
        </div>
        </dialog>
  );
};

export default ModalAgregarTarea;