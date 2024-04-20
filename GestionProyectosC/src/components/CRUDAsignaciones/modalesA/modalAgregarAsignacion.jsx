import React, {useState} from 'react';
import './modal.css';
import api from '../../../api';

const ModalAgregarAsignacion = () => {

  const [Idconsultor, setIdConsultor] = useState('');
  const [proyectoId, setProyectoId] = useState('');
  const [fechaAsignacion, setFechaAsignacion] = useState('');
  const [registroExitoso, setRegistroExitoso] = useState(false);

  const handleAgregarAsignacion = async (e) => {
      e.preventDefault();

      const data = {
      Idconsultor,
      proyectoId,
      fechaAsignacion,
      };

      try {
      const response = await api.post('/Asignacion', data);
      console.log(response);
      setRegistroExitoso(true);
      const timeoutId = setTimeout(() => {
          handleCerrarModal();
      }, 1000);
      setIdConsultor('');
      setProyectoId('');
      setFechaAsignacion('');
      return () => clearTimeout(timeoutId);
      } catch (error) {
      console.error(error);
      }
  };

  const handleCerrarModal = () => {
      const modal = document.getElementById('modalAgregarAsignacion');
      modal.close();
    };

return (
      <dialog id="modalAgregarAsignacion">
          <button type="button" className="btnCerrarModal" onClick={handleCerrarModal}>
          <span className="material-symbols-outlined btnCerrar">
              close
          </span>
      </button>
      <div id="container">
        <h2>Agregar nuevo Asignacion</h2>
        <p>Complete los campos a continuación para registrar un nuevo Asignacion.</p>
        <form action="" id="registrarAsignacion" onSubmit={handleAgregarAsignacion}>
          <label htmlFor="IdConsultor">
            Consultor
            <input
              type="number"
              id="Idconsultor"
              value={parseInt(Idconsultor)}
              onChange={(e) => setIdConsultor(e.target.value)}
              required
            />
          </label>
          <label htmlFor="proyectoId">
            Proyecto
            <input
              type="number"
              id="proyectoId"
              value={proyectoId}
              onChange={(e) => setProyectoId(e.target.value)}
              required
            />
          </label>
          <label htmlFor="fechaAsignacion">
            Fecha de asignacion
            <input
              type="date"
              id="fechaAsignacion"
              value={fechaAsignacion}
              onChange={(e) => setFechaAsignacion(e.target.value)}
              required
            />
          </label>
          {registroExitoso && (
          <div id="successMessage">
            <p>¡Registro de nuevo Asignacion exitoso!</p>
          </div>
        )}
        </form>
        <button type="submit" form="registrarAsignacion">
            Guardar
          </button>
      </div>
      </dialog>
);
}

export default ModalAgregarAsignacion;