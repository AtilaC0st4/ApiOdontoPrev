
# OdontoPrev API

## Integrantes do Grupo
- Átila Costa - RM552650
- Gabriel Leal - RM553779
- Gabriel Plasa - RM553527


---

## Descrição do Projeto
A **OdontoPrev API** é uma aplicação desenvolvida em **ASP.NET Core Web API** que permite aos usuários registrar escovações de dentes e acumular pontos. A cada registro de escovação (manhã, tarde ou noite), o usuário ganha 10 pontos. A API também oferece endpoints para gerenciar usuários e seus registros de escovação.

---

## Arquitetura
A API foi desenvolvida utilizando uma **arquitetura monolítica**, escolhida por ser a mais adequada para o escopo do projeto. Abaixo estão os principais motivos para essa escolha:

### Justificativa da Arquitetura Monolítica
1. **Simplicidade**: A arquitetura monolítica é mais fácil de desenvolver, testar e implantar para projetos de pequeno e médio porte.
2. **Manutenção**: Como o projeto não possui requisitos complexos de escalabilidade ou isolamento de funcionalidades, uma arquitetura monolítica é mais eficiente.
3. **Integração**: Todos os componentes (controllers, models, serviços) estão no mesmo projeto, o que facilita a comunicação entre eles.

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
   git clone https://github.com/AtilaC0st4/ApiOdontoPrev.git
   cd ApiOdontoPrev
   ```

2. **Configure o banco de dados**:
   - Altere a string de conexão no arquivo `appsettings.json` para apontar para o seu banco de dados Oracle:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Sua String de conexão"
     }
     ```

3. **Execute as migrações** (se necessário):
   - No terminal, execute o seguinte comando para aplicar as migrações:
     ```bash
     dotnet ef database update
     ```

4. **Execute a API**:
   - No terminal, execute:
     ```bash
     dotnet run
     ```
   - A API estará disponível em `https://localhost:7104`.

5. **Acesse a documentação**:
   - Abra o navegador e acesse `https://localhost:7104/swagger` para visualizar a documentação interativa da API.

---

## Exemplos de Testes

### Testes Manuais com Swagger
1. **Criar um Usuário**:
   - Endpoint: `POST /api/Users`
   - Body:
     ```json
     {
       "name": "João Silva",
       "points": 0
     }
     ```

2. **Adicionar um Registro de Escovação**:
   - Endpoint: `POST /api/BrushingRecords`
   - Body:
     ```json
     {
       "brushingTime": "2023-10-01T08:00:00",
       "period": "Morning",
       "userId": 1
     }
     ```

3. **Verificar Pontos do Usuário**:
   - Endpoint: `GET /api/Users/1`
   - Resposta esperada:
     ```json
     {
       "id": 1,
       "name": "João Silva",
       "points": 10,
       "brushingRecords": [
         {
           "id": 1,
           "brushingTime": "2023-10-01T08:00:00",
           "period": "Morning",
           "userId": 1
         }
       ]
     }
     ```

