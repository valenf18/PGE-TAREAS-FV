# Tareas PGE - MAUIExcepciones

Este repositorio contiene las tareas desarrolladas para la materia **Programación y Gestión de Entornos (PGE)**, aplicadas sobre un proyecto MAUI multiplataforma. Cada módulo fue diseñado con foco en validación funcional, manejo de excepciones, trazabilidad técnica y control visual.

---

## 1. ANTES DE PROGRAMAR

Antes de escribir código, definí claramente qué quería lograr en cada módulo. Usé lenguaje natural para pensar la lógica y anticipar los errores posibles.

### Ejemplo de prompt que me planteé:
> “Quiero agregar una operación de raíz cuadrada a mi calculadora en MAUI. Si el número es negativo debe lanzar `ArgumentOutOfRangeException` y mostrar un mensaje al usuario. ¿Cómo lo implementaría?”

También me propuse:
- Simular excepciones comunes y mostrar cómo prevenirlas.
- Validar credenciales en un login con control de intentos fallidos.
- Leer, guardar y duplicar archivos con manejo de streams.
- Documentar cada decisión técnica y justificarla ante posibles revisiones.

Guardé estos planteos como parte del README para dejar registro del enfoque previo a la implementación.

---

## 2. MÓDULOS IMPLEMENTADOS

### 📌 Calculadora Segura
- Validación de entrada para raíz cuadrada y división.
- Manejo de `ArgumentOutOfRangeException`, `DivideByZeroException`, `FormatException`.
- Mensajes claros al usuario y log técnico.

### 📌 Login Validado
- Validación de usuario y clave con guard clauses.
- Control de intentos fallidos y bloqueo temporal.
- Manejo de `HttpRequestException`, `TaskCanceledException`.
- Registro en log con tipo de falla y timestamp.

### 📌 Gestor de Archivos
- Guardado, apertura y duplicación de archivos en `AppDataDirectory`.
- Uso de `StreamReader` y `StreamWriter` con `using`.
- Validaciones de existencia, formato y nombre.
- Log técnico y visualización de metadatos.

### 📌 Excepciones Comunes
- Simulación de 6 excepciones: `ArgumentNullException`, `ArgumentOutOfRangeException`, `InvalidOperationException`, `FormatException`, `NullReferenceException`, `IndexOutOfRangeException`.
- Prevención con guard clauses.
- Labels explicativos debajo de cada botón.

---

## 3. DESPUÉS DE PROGRAMAR

### ¿Qué le pedí al asistente?
Planteé ejemplos funcionales en lenguaje natural, como cómo validar una raíz cuadrada o cómo simular una excepción. También pedí ayuda para estructurar el código con buenas prácticas.

### ¿Qué respuesta me dio?
Me ofreció bloques de código claros, con validaciones tempranas, manejo de errores específicos y registro técnico. Siempre con títulos simples y explicaciones limpias.

### ¿Qué tuve que cambiar yo?
Adapté cada bloque al estilo del proyecto, integré los módulos en la arquitectura general, ajusté nombres, rutas y visuales. También revisé el tono de los mensajes para que reflejen mi identidad técnica.

### ¿Qué aprendí de las excepciones y validaciones?
Que prevenir es más importante que capturar. Las guard clauses simplifican la lógica y evitan errores silenciosos. También aprendí a mapear excepciones a mensajes de dominio y a registrar cada evento para trazabilidad.

---
