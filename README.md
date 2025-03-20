
# OdontoPrev API

## Integrantes do Grupo
- Átila Costa - RM552650
- Gabriel Leal - RM553779
- Gabriel Plasa - RM553527


---

## Descrição do Projeto
A **OdontoPrev API** é uma aplicação desenvolvida em **ASP.NET Core Web API** que permite aos usuários registrar escovações de dentes. A API também oferece endpoints para gerenciar usuários e seus registros de escovação.

---

## Arquitetura
A API foi desenvolvida utilizando uma **arquitetura monolítica**, escolhida por ser a mais adequada para o escopo do projeto. Abaixo estão os principais motivos para essa escolha:

### Justificativa da Arquitetura Monolítica
1. **Simplicidade**: A arquitetura monolítica é mais fácil de desenvolver, testar e implantar para projetos de pequeno e médio porte.
2. **Manutenção**: Como o projeto não possui requisitos complexos de escalabilidade ou isolamento de funcionalidades, uma arquitetura monolítica é mais eficiente.
3. **Integração**: Todos os componentes (controllers, models) estão no mesmo projeto, o que facilita a comunicação entre eles.

### Componentes da Arquitetura
- **Controllers**: Responsáveis por receber as requisições HTTP e retornar as respostas adequadas.
- **Models**: Representam as entidades do sistema (usuários e registros de escovação).
- **DbContext**: Gerencia a conexão com o banco de dados Oracle e as operações de CRUD.
- **Swagger/OpenAPI**: Fornece documentação interativa da API.

---

## Design Patterns Utilizados
### Singleton
- **Onde foi aplicado**: No gerenciador de configurações (`AppSettings`), que é injetado como um serviço singleton no `Program.cs`.
- **Justificativa**: O padrão Singleton garante que apenas uma instância do `AppSettings` seja criada e compartilhada em toda a aplicação, evitando duplicação e garantindo consistência.

---

## Instruções para Rodar a API

### Pré-requisitos
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Oracle Database](https://www.oracle.com/database/) (ou um servidor Oracle disponível)
- [Visual Studio](https://visualstudio.microsoft.com/)

### Passos para Executar

1. **Clone o repositório**:  
   ```bash
   git clone https://github.com/AtilaC0st4/ApiOdontoPrevDevops.git
   cd ApiOdontoPrevDevops
   ```

2. **Configure a conexão com o banco de dados**:  
   - No arquivo `appsettings.json`, altere a string de conexão para apontar para seu banco de dados Oracle:  
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Sua_String_de_Conexão"
     }
     ```  

3. **Aplicar Migrações (se necessário)**:  
   Execute os seguintes comandos para gerar e aplicar as migrações no banco de dados:  
   ```bash
   dotnet ef migrations add NomeDaMigration
   dotnet ef database update
   ```  

4. **Executar a API**:  
   ```bash
   dotnet run
   ```  
   A API estará disponível localmente em `http://localhost:7104` ou `https://localhost:7104`.  

5. **Acessar a Documentação**:  
   - Abra o navegador e acesse:  
     🔗 `https://localhost:7104/swagger/index.html`  


---

# Testes da API usando Swagger

Este documento descreve como testar os endpoints das APIs `UsersController` e `BrushingRecordsController` usando o Swagger.

---

## **1. UsersController**

### **1.1. Obter todos os usuários (`GET /api/users`)**
- **Passos:**
  1. Abra a interface do Swagger (geralmente em `https://localhost:<porta>/swagger`).
  2. Localize o endpoint `GET /api/users`.
  3. Clique em "Try it out".
  4. Clique em "Execute".
- **Resultado Esperado:**
  - Se houver usuários cadastrados, retorna uma lista de usuários.
  - Se não houver usuários, retorna `404 Not Found` com a mensagem "Nenhum usuário encontrado".

---

### **1.2. Obter um usuário pelo ID (`GET /api/users/{id}`)**
- **Passos:**
  1. Localize o endpoint `GET /api/users/{id}`.
  2. Clique em "Try it out".
  3. No campo `id`, insira o ID de um usuário existente.
  4. Clique em "Execute".
- **Resultado Esperado:**
  - Se o usuário existir, retorna os detalhes do usuário.
  - Se o usuário não existir, retorna `404 Not Found` com a mensagem "Usuário não encontrado".

---

### **1.3. Criar um novo usuário (`POST /api/users`)**
- **Passos:**
  1. Localize o endpoint `POST /api/users`.
  2. Clique em "Try it out".
  3. No campo `body`, insira os dados do usuário no formato JSON. Exemplo:
     ```json
     {
       "id": 1,
       "name": "João Silva",
       "points": 100
     }
     ```
  4. Clique em "Execute".
- **Resultado Esperado:**
  - Se os dados forem válidos, retorna `201 Created` com os detalhes do usuário criado.
  - Se os dados forem inválidos, retorna `400 Bad Request` com a mensagem "Dados inválidos fornecidos".

---

### **1.4. Atualizar um usuário existente (`PUT /api/users/{id}`)**
- **Passos:**
  1. Localize o endpoint `PUT /api/users/{id}`.
  2. Clique em "Try it out".
  3. No campo `id`, insira o ID de um usuário existente.
  4. No campo `body`, insira os dados atualizados no formato JSON. Exemplo:
     ```json
     {
       "id": 1,
       "name": "João Silva Atualizado",
       "points": 200
     }
     ```
  5. Clique em "Execute".
- **Resultado Esperado:**
  - Se o ID da rota corresponder ao ID do usuário e o usuário existir, retorna `200 OK` com os detalhes do usuário atualizado.
  - Se o ID da rota não corresponder ao ID do usuário, retorna `400 Bad Request` com a mensagem "O ID da rota não corresponde ao ID do usuário".
  - Se o usuário não existir, retorna `404 Not Found` com a mensagem "Usuário não encontrado".

---

### **1.5. Excluir um usuário (`DELETE /api/users/{id}`)**
- **Passos:**
  1. Localize o endpoint `DELETE /api/users/{id}`.
  2. Clique em "Try it out".
  3. No campo `id`, insira o ID de um usuário existente.
  4. Clique em "Execute".
- **Resultado Esperado:**
  - Se o usuário existir, retorna `204 No Content`.
  - Se o usuário não existir, retorna `404 Not Found` com a mensagem "Usuário não encontrado".

---

## **2. BrushingRecordsController**

### **2.1. Obter todos os registros de escovação (`GET /api/brushingrecords`)**
- **Passos:**
  1. Localize o endpoint `GET /api/brushingrecords`.
  2. Clique em "Try it out".
  3. Clique em "Execute".
- **Resultado Esperado:**
  - Retorna uma lista de registros de escovação.

---

### **2.2. Obter um registro de escovação pelo ID (`GET /api/brushingrecords/{id}`)**
- **Passos:**
  1. Localize o endpoint `GET /api/brushingrecords/{id}`.
  2. Clique em "Try it out".
  3. No campo `id`, insira o ID de um registro existente.
  4. Clique em "Execute".
- **Resultado Esperado:**
  - Se o registro existir, retorna os detalhes do registro.
  - Se o registro não existir, retorna `404 Not Found`.

---

### **2.3. Criar um novo registro de escovação (`POST /api/brushingrecords`)**
- **Passos:**
  1. Localize o endpoint `POST /api/brushingrecords`.
  2. Clique em "Try it out".
  3. No campo `body`, insira os dados do registro no formato JSON. Exemplo:
     ```json
     {
       "id": 1,
       "brushingTime": "2023-10-01T08:00:00",
       "period": "Manhã"
     }
     ```
  4. Clique em "Execute".
- **Resultado Esperado:**
  - Se os dados forem válidos, retorna `201 Created` com os detalhes do registro criado.
  - Se os dados forem inválidos, retorna `400 Bad Request`.

---

### **2.4. Atualizar um registro de escovação (`PUT /api/brushingrecords/{id}`)**
- **Passos:**
  1. Localize o endpoint `PUT /api/brushingrecords/{id}`.
  2. Clique em "Try it out".
  3. No campo `id`, insira o ID de um registro existente.
  4. No campo `body`, insira os dados atualizados no formato JSON. Exemplo:
     ```json
     {
       "id": 1,
       "brushingTime": "2023-10-01T08:30:00",
       "period": "Tarde"
     }
     ```
  5. Clique em "Execute".
- **Resultado Esperado:**
  - Se o ID da rota corresponder ao ID do registro e o registro existir, retorna `200 OK` com os detalhes do registro atualizado.
  - Se o ID da rota não corresponder ao ID do registro, retorna `400 Bad Request`.
  - Se o registro não existir, retorna `404 Not Found`.

---

### **2.5. Excluir um registro de escovação (`DELETE /api/brushingrecords/{id}`)**
- **Passos:**
  1. Localize o endpoint `DELETE /api/brushingrecords/{id}`.
  2. Clique em "Try it out".
  3. No campo `id`, insira o ID de um registro existente.
  4. Clique em "Execute".
- **Resultado Esperado:**
  - Se o registro existir, retorna `204 No Content`.
  - Se o registro não existir, retorna `404 Not Found`.

---

## **Exemplo de Teste Completo para BrushingRecordsController**

1. **Criar um novo registro de escovação:**
   - **Endpoint:** `POST /api/brushingrecords`
   - **Body:**
     ```json
     {
       "id": 1,
       "brushingTime": "2023-10-01T08:00:00",
       "period": "Manhã"
     }
     ```
   - **Resultado Esperado:** `201 Created` com os detalhes do registro criado.

2. **Obter o registro criado:**
   - **Endpoint:** `GET /api/brushingrecords/1`
   - **Resultado Esperado:** `200 OK` com os detalhes do registro.

3. **Atualizar o registro:**
   - **Endpoint:** `PUT /api/brushingrecords/1`
   - **Body:**
     ```json
     {
       "id": 1,
       "brushingTime": "2023-10-01T08:30:00",
       "period": "Tarde"
     }
     ```
   - **Resultado Esperado:** `200 OK` com os detalhes do registro atualizado.

4. **Excluir o registro:**
   - **Endpoint:** `DELETE /api/brushingrecords/1`
   - **Resultado Esperado:** `204 No Content`.

5. **Tentar obter o registro excluído:**
   - **Endpoint:** `GET /api/brushingrecords/1`
   - **Resultado Esperado:** `404 Not Found`.

---


