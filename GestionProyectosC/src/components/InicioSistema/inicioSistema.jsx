// import React, { useState, useEffect } from 'react';
// import './inicioSistema.css'
// import api from '../../api';

// const inicioSistema = () => {

//     const [proyectos, setProyectos] = useState([]);

//     const fetchProyectos = async () => {
//         try {
//         const response = await api.get('/Proyecto');
//         setProyectos(response.data);
//         } catch (error) {
//         console.error(error);
//         }
//     };

//     useEffect(() => {
//         fetchProyectos();
//     }, []);

//   return (
//     <div id="containerInicio">
//         <div id="containerProyectos">
//             <h2>Proyectos registrados</h2>
//             <div id="listadoProyectos">
//             {proyectos.length > 0 ? (
//             proyectos.map((proyecto) => (
//               <div className="proyecto" key={proyecto.id}>
//                 <div>
//                   <p className="nombre">{proyecto.nombre}</p>
//                   <p className="estado">{proyecto.estado}</p>
//                 </div>
//               </div>
//             ))
//           ) : (
//             <p>No hay proyectos registrados.</p>
//           )}
//             </div>
//         </div>
//         <div id="containerAsignaciones">
            
//         </div>
//     </div>
//   );
// };

// export default inicioSistema;


import React, { useState, useEffect } from 'react';
import './inicioSistema.css';
import api from '../../api';

const InicioSistema = () => {
  const [proyectos, setProyectos] = useState([]);
  const [consultores, setConsultores] = useState([]);

  const fetchProyectos = async () => {
    try {
      const response = await api.get('/Proyecto');
      setProyectos(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  const fetchConsultores = async () => {
    try {
      const response = await api.get('/Consultor');
      setConsultores(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  useEffect(() => {
    fetchProyectos();
    fetchConsultores();
  }, []);

  return (
    <div id="containerInicio">
      <div id="containerProyectos">
        <h2>Proyectos registrados</h2>
        <div id="listadoProyectos">
          {proyectos.length > 0 ? (
            proyectos.map((proyecto) => (
              <div className="proyecto" key={proyecto.id}>
                <div>
                  <p className="nombre">{proyecto.nombre}</p>
                  <p className="estado">{proyecto.estado}</p>
                </div>
              </div>
            ))
          ) : (
            <p>No hay proyectos registrados.</p>
          )}
        </div>
      </div>
      <div id="containerConsultores">
        <h2>Consultores</h2>
        <div id="listadoConsultores">
          {consultores.length > 0 ? (
            consultores.map((consultor) => (
              <div className="consultor" key={consultor.id}>
                <div>
                  <p className="nombre">{consultor.nombre}</p>
                  <p className="especialidad">{consultor.especialidad}</p>
                </div>
              </div>
            ))
          ) : (
            <p>No hay consultores registrados.</p>
          )}
        </div>
      </div>
    </div>
  );
};

export default InicioSistema;
