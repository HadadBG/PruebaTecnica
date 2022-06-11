using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prueba_Tecnica
{
    static class Querys
    {
       public static string get_topUsers = "Select u.nombre,e.sueldo from empleados e LEFT JOIN usuarios u on e.userId = u.userId "+ 
                                        "order by e.sueldo desc limit 10;";

        public static string get_info = @"Select u.Login as 'username',
                                          Concat(u.Nombre,' ' , u.Paterno,' ', u.Materno ) as 'nombreCompleto',
                                          e.Sueldo as 'sueldo',
                                          e.FechaIngreso as 'fechaIngreso'
                                          from usuarios u
                                          LEFT JOIN empleados e ON e.userId = u.userId ;";
        public static string update_user = @"UPDATE empleados e
                                             LEFT JOIN usuarios u ON e.userId = u.userId 
                                             SET e.sueldo = @pSalario WHERE u.Login = @pUser;";

    }
}
