
# 🦷 OdontoPrev API

A **OdontoPrev API** é uma aplicação desenvolvida em ASP.NET Core Web API que permite aos usuários registrar escovações de dentes. A API também oferece funcionalidades para gerenciamento de usuários e escovações, visando promover hábitos saudáveis de higiene bucal. Recentemente, foram integradas funcionalidades de **IA generativa** para melhorar a experiência do usuário, além de seguir boas práticas de **Clean Code** e cobertura de testes.

---

## 👥 Integrantes do Grupo

- Átila Costa - RM552650  
- Gabriel Leal - RM553779  
- Gabriel Plasa - RM553527  

---

## 🛠️ Tecnologias Utilizadas

- ASP.NET Core Web API  
- Entity Framework Core  
- Swagger  
- xUnit  
- AutoMapper  
- RESTful APIs  
- Clean Architecture  

---

## 📁 Estrutura do Projeto

- **Controllers**: Recebem requisições e retornam respostas HTTP.
- **Models / DTOs**: Classes que representam a estrutura dos dados e objetos de transferência.
- **Services**: Contém as regras de negócio da aplicação.
- **Data**: Configuração do contexto de banco e migrações.
- **Migrations**: Controle de versão do banco via EF Core.
- **Tests**: Testes unitários e de integração com xUnit.

---

## ✅ Funcionalidades

- Registro e histórico de escovações.
- Cadastro, atualização e exclusão de usuários.
- Consulta de dados por ID ou filtros.
- Integração com a API ViaCEP para busca de endereço por CEP.
- **IA Generativa**: Sugestões personalizadas de cuidados com a saúde bucal baseadas nos registros do usuário.
- Autenticação básica e validação de dados.
- API documentada via Swagger.

---

## 🤖 Funcionalidades de IA Generativa

A API agora conta com um endpoint que utiliza IA para fornecer dicas personalizadas sobre cuidados com os dentes, baseando-se no histórico de escovações do usuário:

- **Geração de recomendações dinâmicas**
- **Sugestões de rotina saudável**
- **Feedback automático com base em frequência de escovação**

> **Tecnologia utilizada:** com integração customizada no serviço `AiService`.

---

## 💡 Práticas de Clean Code

O projeto segue os princípios de **Clean Code**, como:

- Métodos curtos e com nomes descritivos.
- Separação de responsabilidades (SRP).
- Injeção de dependência.
- Padronização de DTOs e Models.
- Evita código duplicado e promove reutilização.
- Convenções de nomenclatura consistentes.
- Comentários apenas quando estritamente necessário.

---

## 🧪 Testes

Os testes implementados garantem a confiabilidade da aplicação, cobrindo:

### ✔️ Testes Unitários

- Testes de serviços (`UserService`, `EscovacaoService`, `AiService`).
- Mock de dependências com uso de `Moq`.
- Verificação de regras de negócio.

### 🧪 Testes de Integração

- Testes da API utilizando `WebApplicationFactory`.
- Banco em memória para simular chamadas reais.
- Validação de rotas, status HTTP e respostas.

### 🔬 Testes de Sistema (em andamento)

- Simulação de fluxo completo (registro de usuário + escovação + recomendação de IA).
- Planejamento de testes com Postman/Newman para CI/CD.

> Para executar os testes:
```bash
dotnet test
```

---

## 🚀 Como Executar o Projeto

```bash
# Clonar o repositório
git clone https://github.com/AtilaC0st4/ApiOdontoPrev.git
cd ApiOdontoPrev

# Restaurar pacotes
dotnet restore

# Aplicar migrações
dotnet ef database update

# Executar o projeto
dotnet run
```

Acesse a documentação interativa em:  
[https://localhost:5001/swagger](https://localhost:5001/swagger)

---

## 📄 Licença

Este projeto está licenciado sob a Licença MIT. Consulte o arquivo [LICENSE](LICENSE) para mais informações.
