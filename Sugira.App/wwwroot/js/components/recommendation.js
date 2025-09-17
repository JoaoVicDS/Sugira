document.addEventListener('DOMContentLoaded', () => {

    // --- Elementos do DOM e Estado (sem alterações) ---
    const startBtn = document.getElementById('startRecommendationBtn');
    const modalElement = document.getElementById('recommendationModal');
    const modalTitle = document.getElementById('recommendationModalLabel');
    const modalBody = document.getElementById('modalBodyContent');
    const modalFooter = document.getElementById('modalFooterContent');
    const modal = new bootstrap.Modal(modalElement);

    let formState = null;
    let userAnswers = { formId: null, categories: [] };
    let currentCategoryIndex = 0;
    let currentQuestionIndex = 0;
    let recommendationState = null; // NOVO: Estado para a recomendação
    let currentRecommendationCategoryIndex = 0; // NOVO: Índice da recomendação atual

    // ETAPA 1: Abrir modal e carregar menus
    startBtn.addEventListener('click', async () => {
        resetState();
        showLoading('Carregando menus...');
        modal.show();
        try {
            const response = await fetch('/recommendation/menus');
            if (!response.ok) throw new Error('Falha ao buscar menus.');
            const menus = await response.json();
            renderMenuSelection(menus);
        } catch (error) {
            showError(error.message);
        }
    });

    function renderMenuSelection(menus) {
        modalTitle.textContent = 'Iniciar Recomendação';
        modalBody.innerHTML = `
            <div class="mb-3">
                <label for="menuSelect" class="form-label">Para qual menu você deseja uma recomendação?</label>
                <select id="menuSelect" class="form-select">
                    <option selected disabled value="">Selecione um menu...</option>
                    ${menus.map(menu => `<option value="${menu.id}">${menu.name}</option>`).join('')}
                </select>
            </div>
            <div class="mb-3">
                <label for="questionLimitSelect" class="form-label">Número de perguntas por categoria:</label>
                <select id="questionLimitSelect" class="form-select">
                    <option value="">Todas</option>
                    <option value="1">1 Pergunta (Rápido)</option>
                    <option value="2">2 Perguntas</option>
                    <option value="3" selected>3 Perguntas (Recomendado)</option>
                    <option value="4">4 Perguntas</option>
                </select>
                <div class="form-text text-muted mt-2">
                    <i class="bi bi-info-circle-fill"></i> Quanto mais perguntas, mais precisa será a sua recomendação.
                </div>
            </div>
        `;
        modalFooter.innerHTML = `<button id="nextStepBtn" class="btn btn-primary">Avançar</button>`;
        document.getElementById('nextStepBtn').addEventListener('click', handleStartRecommendation);
    }

    // ETAPA 2: Iniciar formulário
    async function handleStartRecommendation() {
        const menuId = document.getElementById('menuSelect').value;
        const questionLimit = document.getElementById('questionLimitSelect').value;

        if (!menuId) { alert('Por favor, selecione um menu.'); return; }

        showLoading('Gerando seu formulário...');

        const requestBody = {
            menuId: parseInt(menuId),
            questionLimit: questionLimit ? parseInt(questionLimit) : null
        };

        try {
            const response = await fetch('/recommendation/start', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(requestBody)
            });
            if (!response.ok) throw new Error('Falha ao gerar o formulário.');
            formState = await response.json();
            userAnswers.formId = formState.formId;
            renderCurrentQuestion();
        } catch (error) {
            showError(error.message);
        }
    }

    // ETAPA 3: Renderizar a pergunta atual
    function renderCurrentQuestion(isNewCategory = false) {
        const category = formState.categories[currentCategoryIndex];
        const question = category.questions[currentQuestionIndex];
        const totalQuestionsInCategory = category.questions.length;

        modalTitle.textContent = category.name;

        const questionHtml = `
            <div class="question-container fade-in-up">
                <p class="question-text">${question.question}</p>
                <div class="radio-options-container">
                    ${question.options.map((opt, index) => `
                        <div class="radio-option">
                            <input type="radio" id="option${index}" name="${question.characteristic}" value="${opt}" required>
                            <label for="option${index}">${opt}</label>
                        </div>
                    `).join('')}
                </div>
            </div>
            <div class="question-counter">${currentQuestionIndex + 1} de ${totalQuestionsInCategory}</div>
        `;

        if (isNewCategory) {
            modalBody.innerHTML = `<h3 class="category-title-animation">${category.name}</h3>`;
            setTimeout(() => {
                modalBody.innerHTML = questionHtml;
            }, 1500); // Delay para mostrar a pergunta após o título
        } else {
            modalBody.innerHTML = questionHtml;
        }

        modalFooter.innerHTML = `<button id="nextQuestionBtn" class="btn btn-primary">Próximo</button>`;
        document.getElementById('nextQuestionBtn').addEventListener('click', handleNextQuestion);
    }

    // ETAPA 4: Avançar para a próxima pergunta ou submeter
    function handleNextQuestion() {
        const category = formState.categories[currentCategoryIndex];
        const question = category.questions[currentQuestionIndex];
        const selectedOption = document.querySelector(`input[name="${question.characteristic}"]:checked`);

        if (!selectedOption) { alert('Por favor, selecione uma opção.'); return; }

        // Salva a resposta
        let categoryAnswers = userAnswers.categories.find(c => c.name === category.name);
        if (!categoryAnswers) {
            categoryAnswers = { name: category.name, selectedAnswers: [] };
            userAnswers.categories.push(categoryAnswers);
        }
        categoryAnswers.selectedAnswers.push({
            characteristicAsked: question.characteristic,
            selectedOption: selectedOption.value
        });

        // Avança para a próxima pergunta/categoria
        currentQuestionIndex++;
        if (currentQuestionIndex >= category.questions.length) {
            currentCategoryIndex++;
            currentQuestionIndex = 0;
            if (currentCategoryIndex >= formState.categories.length) {
                handleSubmitAnswers(); // Fim de todas as perguntas
            } else {
                renderCurrentQuestion(true); // Nova categoria
            }
        } else {
            renderCurrentQuestion(false); // Próxima pergunta
        }
    }

    // ETAPA 5: Submeter respostas e mostrar recomendação
    async function handleSubmitAnswers() {
        showLoading('Analisando suas preferências...');
        try {
            const response = await fetch('/recommendation/submit', {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(userAnswers)
            });
            if (!response.ok) throw new Error('Falha ao obter a recomendação.');
            const recommendation = await response.json();
            renderRecommendation(recommendation);
        } catch (error) {
            showError(error.message);
        }
    }

    function renderRecommendation(rec) {
        recommendationState = rec; // Salva o resultado completo
        currentRecommendationCategoryIndex = 0; // Reseta o índice
        renderCurrentRecommendation();
    }

    function renderCurrentRecommendation() {
        const categoryRec = recommendationState.categories[currentRecommendationCategoryIndex];

        modalTitle.textContent = 'Aqui está sua Sugestão!';

        // ETAPA 1: Renderiza o container e o título da categoria.
        // O título da categoria já vem com sua classe de animação.
        modalBody.innerHTML = `
        <h3 class="category-title-animation">${categoryRec.name}</h3>
        <div id="recommendation-card-container"></div>
    `;

        const cardContainer = document.getElementById('recommendation-card-container');

        // ETAPA 2: Cria o HTML para o card da recomendação.
        const recHtml = categoryRec.items.map(item => `
        <div class="recommendation-card fade-in-up">
            <div class="rec-icon">
                <i class="bi bi-star-fill"></i>
            </div>
            <div class="rec-text">
                <span>Nossa sugestão para você é:</span>
                <strong>${item.recommendation}</strong>
            </div>
        </div>
    `).join('');

        // ETAPA 3: Após o delay, insere SOMENTE o card da recomendação no seu container.
        // Isso evita que o título seja renderizado novamente.
        setTimeout(() => {
            cardContainer.innerHTML = recHtml;
        }, 600); // Atraso para o card aparecer depois do título

        // Lógica dos botões (permanece a mesma)
        const isLastCategory = currentRecommendationCategoryIndex === recommendationState.categories.length - 1;
        if (isLastCategory) {
            modalFooter.innerHTML = `<button type="button" class="btn btn-success" data-bs-dismiss="modal">Finalizar</button>`;
        } else {
            modalFooter.innerHTML = `<button id="nextRecBtn" class="btn btn-primary">Próxima Categoria</button>`;
            document.getElementById('nextRecBtn').addEventListener('click', handleNextRecommendation);
        }
    }


    function handleNextRecommendation() {
        currentRecommendationCategoryIndex++;
        if (currentRecommendationCategoryIndex < recommendationState.categories.length) {
            renderCurrentRecommendation();
        }
    }

    // --- Funções de Utilidade (com pequenas melhorias) ---
    function resetState() {
        formState = null;
        userAnswers = { formId: null, categories: [] };
        currentCategoryIndex = 0;
        currentQuestionIndex = 0;
        recommendationState = null;
        currentRecommendationCategoryIndex = 0;
    }
    function showLoading(message) {
        modalTitle.textContent = 'Aguarde...';
        modalBody.innerHTML = `<div class="d-flex justify-content-center align-items-center flex-column p-5"><div class="spinner-border text-primary" role="status"></div><p class="mt-3 text-muted">${message}</p></div>`;
        modalFooter.innerHTML = '';
    }
    function showError(message) {
        modalTitle.textContent = 'Ocorreu um Erro';
        modalBody.innerHTML = `<div class="alert alert-danger">${message}</div>`;
        modalFooter.innerHTML = `<button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Fechar</button>`;
    }
});