# Banquetes

**Pedidos Banquetes** es un sistema de gestión para levantamiento de pedidos y control de eventos, diseñado para empresas de banquetes o servicios de catering. Permite administrar clientes, artículos, kits o paquetes, anticipos, entregas y metas de venta, centralizando toda la operación en una sola aplicación de escritorio.

---

## Características principales

- **Levantamiento de pedidos:** Registro ágil de pedidos de clientes con información detallada de artículos, kits y cantidades.
- **Gestión de artículos y kits:** Alta, edición y ensamblado de artículos en paquetes o kits personalizados.
- **Gestión de clientes:** Control de datos generales, fiscales y de facturación (RFC, régimen fiscal, código postal).
- **Cobranza y control de saldos:** Registro de anticipos, pagos parciales y liquidaciones con cálculo automático de saldo pendiente.
- **Reportes administrativos:** Visualización de pedidos activos, entregados, pagos recibidos y metas de ventas.
- **Seguimiento de entrega:** Control visual del estado del pedido: “En espera de cliente”, “Cliente llegó” o “Pedido entregado”.
- **Forecast de ventas:** Registro y comparación de metas vs ventas reales por periodo.
- **Control de usuarios y roles:** Permisos personalizados para administración, ventas y ensamblaje.

---

## Arquitectura del sistema

El sistema está desarrollado con arquitectura **MVC (Modelo-Vista-Controlador)** y patrón **DAO (Data Access Object)** para el manejo de datos.

- **Modelo:** Clases que representan entidades del negocio (Cliente, Artículo, Pedido, Kit, Pago).
- **Vista:** Interfaz gráfica desarrollada en **Windows Forms**, con estilos personalizados mediante la clase `UIStyles.cs`.
- **Controlador:** Coordina la interacción entre la vista y el modelo.
- **DAO:** Gestiona la conexión con la base de datos MySQL y la ejecución de consultas SQL seguras.

---

## Tecnologías utilizadas

| Componente | Descripción |
|-------------|--------------|
| **Lenguaje** | C# (.NET Framework 4.7.2) |
| **Base de datos** | MySQL |
| **Interfaz gráfica** | Windows Forms |
| **ORM / DAO** | Acceso directo mediante clases DAO personalizadas |
| **Control de versiones** | Git / GitHub |
| **Diseño visual** | UIStyles personalizados con colores y fuentes modernos |

---

## Instalación y configuración

### 🔹 Requisitos previos

- Visual Studio 2019 o superior  
- .NET Framework 4.7.2  
- MySQL Server (versión 5.7 o superior)  
- Conexión a base de datos configurada en el archivo de conexión (por ejemplo: `ConnectionFactory.cs`)

### 🔹 Pasos de instalación

1. **Clonar el repositorio:**
   ```bash
   [git clone https://github.com/usuario/PedidosPreOrder.git](https://github.com/RENOVATIO-PyME-DEVS/Control-Pedidos/)

2. **Abrir la solución:**

   * Abre `Control_Pedidos.sln` desde Visual Studio.

3. **Configurar la conexión a base de datos:**

   * Edita la cadena de conexión en `ConnectionFactory.cs`:

     ```csharp
     private const string ConnectionString = "server=localhost;database=banquetes;uid=root;pwd=tu_contraseña;";
     ```

4. **Compilar y ejecutar el proyecto.**

---

## Módulos principales

| Módulo               | Descripción                                                              |
| -------------------- | ------------------------------------------------------------------------ |
| **Clientes**         | Alta, edición y validación fiscal (RFC, régimen fiscal).                 |
| **Artículos / Kits** | Administración de artículos individuales y compuestos.                   |
| **Pedidos**          | Creación, edición, control de estatus y entrega.                         |
| **Cobranza**         | Registro de pagos y control de saldos.                                   |
| **Reportes**         | Consultas y exportaciones de ventas, pedidos y anticipos.                |
| **Usuarios**         | Administración de roles y permisos.                                      |
| **Dashboard**        | Visualización de pedidos del día, estatus de clientes y metas de ventas. |

---

## Flujo de actividades (Workflow general)

1. **Levantar pedido del cliente.**
2. **Alta o selección de artículos y kits.**
3. **Registro de anticipo o pago parcial.**
4. **Confirmación y verificación de llegada del cliente.**
5. **Comienzo del ensamble del pedido.**
6. **Entrega de mercancía.**
7. **Registro de pago final y cierre del pedido.**
8. **Generación de reportes y análisis de ventas.**

---

## Base de datos

Las tablas principales incluyen:

* `clients`
* `items`
* `kits`
* `orders`
* `payments`
* `tax_regime`
* `users`
* `roles`

Cada entidad cuenta con su respectivo DAO (por ejemplo: `ArticuloDao`, `ClienteDao`) para manejo seguro de CRUD.

---

## Estilos de interfaz

El sistema utiliza una clase personalizada `UIStyles.cs` que define:

* Paleta de colores corporativos (fondos, acentos, texto).
* Encabezados visuales.
* Estilos consistentes entre formularios.

Esto mejora la presentación visual sin alterar las proporciones ni estructuras base del formulario.

---

## Roadmap

* [x] Alta y gestión de artículos.
* [x] Creación de pedidos y kits.
* [x] Módulo de cobranza.
* [ ] Dashboard visual con pedidos del día.
* [ ] Módulo de notificaciones.
* [ ] Exportación avanzada a Excel y PDF.

---

##  Contribución

1. Haz un **fork** del repositorio.
2. Crea una **rama** para tu cambio (`git checkout -b feature/nueva-funcionalidad`).
3. Realiza los cambios y **haz commit** (`git commit -m "Descripción del cambio"`).
4. Envía un **pull request**.

---

## Licencia

Este proyecto está bajo la licencia **MIT**.
Puedes usarlo, modificarlo y distribuirlo libremente con atribución al autor original.

---

## Autor

**Desarrollado por:**
Equipo de desarrollo *RENOVATIO PyME*
📧 Contacto: [soporte@renovatiopyme.com](mailto:soporte@renovatiopyme.com)

---


