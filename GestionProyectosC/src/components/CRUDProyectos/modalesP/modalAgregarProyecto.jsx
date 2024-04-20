import React, {useState} from 'react';
import './modal.css';
import api from '../../../api';

const ModalAgregarProyecto = ({fetchProyectos}) => {

    const [nombre, setNombre] = useState('');
    const [descripcion, setDescripcion] = useState('');
    const [fechaInicio, setFechaInicio] = useState('');
    const [fechaFin, setFechaFin] = useState('');
    const [estado, setEstado] = useState('No iniciado');
    // const [estado, setEstado] = useState('');
    const [clienteID, setClienteID] = useState('');
    // const [nombreCliente, setNombreCliente] = useState('');

    const [registroExitoso, setRegistroExitoso] = useState(false);

    const handleEstadoChange = (event) => {
      setEstado(event.target.value);
    };

    const handleAgregarProyecto = async (e) => {
        e.preventDefault();

        const data = {
        nombre,
        descripcion,
        fechaInicio,
        fechaFin,
        estado,
        clienteID,
        };

        try {
        const response = await api.post('/Proyecto', data);
        console.log(response);
        setRegistroExitoso(true);
        fetchProyectos();
        const timeoutId = setTimeout(() => {
            handleCerrarModal();
        }, 1000);
        setNombre('');
        setDescripcion('');
        setFechaInicio('');
        setFechaFin('');
        setEstado('');
        setClienteID('');
        return () => clearTimeout(timeoutId);
        } catch (error) {
        console.error(error);
        }
    };

    const handleCerrarModal = () => {
        const modal = document.getElementById('modalAgregarProyecto');
        modal.close();
      };

  return (
        <dialog id="modalAgregarProyecto">
            <button type="button" className="btnCerrarModal" onClick={handleCerrarModal}>
            <span className="material-symbols-outlined btnCerrar">
                close
            </span>
        </button>
        <div id="container">
          <h2>Agregar nuevo Proyecto</h2>
          <p>Complete los campos a continuación para registrar un nuevo Proyecto.</p>
          <form action="" id="registrarProyecto" onSubmit={handleAgregarProyecto}>
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
              <select name="estado" id="estado" value={estado} onChange={handleEstadoChange}>
                <option value="No iniciado">No iniciado</option>
                <option value="En curso">En curso</option>
                <option value="Finalizado">Finalizado</option>
              </select>
            </label>
            <label htmlFor="clienteID">
              Cliente
              <input
                type="number"
                id="clienteID"
                value={parseInt(clienteID)}
                onChange={(e) => setClienteID(e.target.value)}
                required
              />
            </label>
            {registroExitoso && (
            <div id="successMessage">
              <p>¡Registro de nuevo proyecto exitoso!</p>
            </div>
          )}
          </form>
          <button type="submit" form="registrarProyecto">
              Guardar
            </button>
        </div>
        </dialog>
  );
};

export default ModalAgregarProyecto;