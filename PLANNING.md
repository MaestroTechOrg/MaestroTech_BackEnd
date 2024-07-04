# Planejamento do Projeto MaestroTech 🎶💻

## Visão Geral

**MaestroTech** é um sistema avançado de gerenciamento e sorteio de músicas para igrejas, projetado para facilitar a organização dos louvores nos cultos. Utilizando uma abordagem de Clean Architecture e tecnologias modernas, o MaestroTech oferece uma solução robusta e escalável para gerenciar músicas, cultos e preferências de dias.

## Funcionalidades Principais

### Cadastro de Igrejas

- **Descrição**: Funcionalidade para cadastrar novas igrejas, permitindo que cada igreja tenha seu próprio banco de dados e usuários.
- **Critérios de Aceitação**:
  - Campos: Nome, Endereço, Dias de Culto.
  - Acesso restrito a administradores.

### Cadastro de Usuários

- **Descrição**: Funcionalidade para cadastrar e gerenciar usuários do sistema.
- **Critérios de Aceitação**:
  - Campos: Nome, Email, Senha, Perfil (Administrador, Usuário).
  - Autenticação e autorização.

### CRUD de Músicas

- **Descrição**: Funcionalidade para adicionar, editar, visualizar e excluir músicas.
- **Critérios de Aceitação**:
  - Campos: Nome, Link para Spotify, Link para YouTube, Notas, Cantores, Dia de Preferência.

### Configuração de Dias de Culto

- **Descrição**: Funcionalidade para configurar os dias de culto para sorteio de músicas.
- **Critérios de Aceitação**:
  - Configuração por igreja.
  - Dias da semana personalizáveis.

### Sorteio de Músicas

- **Descrição**: Funcionalidade para realizar o sorteio de músicas para os cultos, respeitando as preferências de dia.
- **Critérios de Aceitação**:
  - Sorteio aleatório com base nas preferências.
  - Funcionalidade para sorteios de sexta-feira, domingo e um sábado por mês.

### Envio de Resultados para WhatsApp

- **Descrição**: Funcionalidade para enviar automaticamente os resultados do sorteio para grupos do WhatsApp.
- **Critérios de Aceitação**:
  - Integração com API do WhatsApp (ou outra solução de envio).
  - Configuração por igreja.

## Estrutura do Projeto

O MaestroTech segue os princípios da Clean Architecture, garantindo um código organizado, de fácil manutenção e escalável. As camadas principais são:

1. **Domain**: Entidades e regras de negócio.
2. **Application**: Casos de uso.
3. **Infrastructure**: Acesso a dados e outras implementações técnicas.
4. **API**: Interface de usuário (API).

## Tecnologias Utilizadas

- **Backend**: C# com ASP.NET Core
- **Frontend**: A ser definido (Angular, React, ou outra tecnologia adequada)
- **Banco de Dados**: SQL Server

## Metodologia Ágil

### Sprints

- Definiremos sprints de 2 semanas para entregas incrementais.

### Backlog

- Criação de um backlog de tarefas.
- Issues no GitHub para gerenciar as tarefas.

### Daily Stand-ups

- Reuniões diárias para acompanhamento.

### Reviews e Retrospectives

- Ao final de cada sprint, revisaremos o progresso e ajustaremos o planejamento conforme necessário.

## Backlog Inicial do Produto

### Epic 1: Gerenciamento de Igrejas

- **User Story 1**: Como administrador, quero cadastrar uma nova igreja para gerenciar suas músicas e cultos.
- **User Story 2**: Como administrador, quero editar os dados de uma igreja para manter as informações atualizadas.
- **User Story 3**: Como administrador, quero configurar os dias de cultos para sorteio de músicas.

### Epic 2: Gerenciamento de Usuários

- **User Story 1**: Como administrador, quero cadastrar novos usuários para que possam acessar o sistema.
- **User Story 2**: Como administrador, quero editar os dados dos usuários para manter as informações atualizadas.
- **User Story 3**: Como usuário, quero fazer login no sistema para acessar minhas funcionalidades.

### Epic 3: Gerenciamento de Músicas

- **User Story 1**: Como usuário, quero cadastrar uma nova música com informações detalhadas.
- **User Story 2**: Como usuário, quero editar as informações de uma música existente.
- **User Story 3**: Como usuário, quero excluir uma música que não será mais utilizada.

### Epic 4: Sorteio de Músicas

- **User Story 1**: Como administrador, quero sortear músicas para os cultos de sexta-feira.
- **User Story 2**: Como administrador, quero sortear músicas para os cultos de domingo.
- **User Story 3**: Como administrador, quero sortear músicas para o culto de sábado (uma vez por mês).
- **User Story 4**: Como administrador, quero que o sistema respeite as preferências de dia na escolha das músicas.

### Epic 5: Envio de Resultados

- **User Story 1**: Como administrador, quero enviar a lista de músicas sorteadas para o grupo do WhatsApp da igreja.

## Planejamento das Sprints

### Sprint 1: Configuração Inicial e Cadastro de Igrejas

- Configuração do ambiente de desenvolvimento.
- Criação da estrutura base do projeto seguindo a Clean Architecture.
- Implementação do cadastro e gerenciamento de igrejas.

### Sprint 2: Cadastro de Usuários

- Implementação do cadastro e gerenciamento de usuários.
- Implementação de autenticação e autorização.

### Sprint 3: CRUD de Músicas

- Implementação do cadastro, edição e exclusão de músicas.
- Criação de endpoints de API para gerenciamento de músicas.

### Sprint 4: Sorteio de Músicas

- Implementação da lógica de sorteio de músicas.
- Configuração de dias de cultos para sorteio.

### Sprint 5: Envio de Resultados

- Implementação do envio de resultados para WhatsApp.
- Integração com API do WhatsApp (ou outra solução de envio).

### Sprint 6: Testes e Ajustes Finais

- Testes de integração e aceitação.
- Ajustes e correções finais.
- Documentação do projeto.

## Como Contribuir

### Issues

- Use as issues para reportar bugs, sugerir novas funcionalidades ou discutir melhorias.
- Utilize labels para categorizar as issues (ex: enhancement, bug, documentation).

### Pull Requests

- Fork o repositório e crie um branch para a sua feature ou correção.
- Envie um pull request com uma descrição detalhada das mudanças.

### Documentação

- Colabore com a documentação do projeto através da wiki ou editando o `PLANNING.md`.
- Adicione sugestões de funcionalidades na seção abaixo.

## Sugestões de Funcionalidades

(Adicione sugestões aqui)

---

**MaestroTech** - Harmonizando tecnologia e louvor 🎶💻
