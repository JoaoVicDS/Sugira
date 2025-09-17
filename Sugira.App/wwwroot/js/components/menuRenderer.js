/**
 * Renderiza a lista de características de um item.
 */
function renderCharacteristics(characteristics) {
    if (!characteristics || characteristics.length === 0) {
        return '';
    }
    const items = characteristics
        .map(char => `<li class="characteristic-item"><strong>${char.typeName}:</strong> ${char.option}</li>`)
        .join('');
    return `<ul class="characteristics-list">${items}</ul>`;
}

/**
 * Renderiza um único item do menu com o placeholder da foto.
 */
function renderItemWithPhoto(item) {
    const photoElement = `<div class="item-photo-placeholder"><span>Sem foto</span></div>`;

    return `
        <div class="item-card">
            <div class="item-photo">${photoElement}</div>
            <div class="item-details">
                <h4 class="item-name">${item.name}</h4>
                ${renderCharacteristics(item.characteristics)}
            </div>
        </div>
    `;
}

/**
 * NOVO: Renderiza uma única categoria como um item de accordion aninhado.
 * @param {object} category - O objeto da categoria.
 * @param {number} menuId - O ID do menu pai, para garantir IDs únicos.
 * @returns {string} O HTML do item de accordion da categoria.
 */
function renderCategoryAsAccordionItem(category, menuId) {
    const collapseId = `category-collapse-${menuId}-${category.id}`;
    const headingId = `category-heading-${menuId}-${category.id}`;

    return `
        <div class="accordion-item category-accordion-item">
            <h2 class="accordion-header" id="${headingId}">
                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#${collapseId}" aria-expanded="false" aria-controls="${collapseId}">
                    ${category.name}
                </button>
            </h2>
            <div id="${collapseId}" class="accordion-collapse collapse" aria-labelledby="${headingId}" data-bs-parent="#categoryAccordion-${menuId}">
                <div class="accordion-body">
                    <div class="items-container">
                        ${category.items.map(renderItemWithPhoto).join('')}
                    </div>
                </div>
            </div>
        </div>
    `;
}

/**
 * ALTERADO: Renderiza um único menu, que agora contém um accordion de categorias.
 * @param {object} menu - O objeto do menu.
 * @returns {string} O HTML do item de accordion do menu.
 */
function renderSingleMenuAsAccordionItem(menu) {
    const menuCollapseId = `menu-collapse-${menu.id}`;
    const menuHeadingId = `menu-heading-${menu.id}`;
    const categoryAccordionId = `categoryAccordion-${menu.id}`;

    return `
        <div class="accordion-item">
            <h2 class="accordion-header" id="${menuHeadingId}">
                <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target="#${menuCollapseId}" aria-expanded="false" aria-controls="${menuCollapseId}">
                    ${menu.name}
                </button>
            </h2>
            <div id="${menuCollapseId}" class="accordion-collapse collapse" aria-labelledby="${menuHeadingId}" data-bs-parent="#menuAccordion">
                <div class="accordion-body">
                    <div class="accordion" id="${categoryAccordionId}">
                        ${menu.categories.map(category => renderCategoryAsAccordionItem(category, menu.id)).join('')}
                    </div>
                </div>
            </div>
        </div>
    `;
}

/**
 * Renderiza todos os menus em um contêiner de accordion.
 */
function renderMenu(menusData, container) {
    if (!menusData || menusData.length === 0) {
        container.innerHTML = '<p class="error-message">Nenhum menu disponível no momento.</p>';
        return;
    }
    const accordionHtml = `
        <div class="accordion" id="menuAccordion">
            ${menusData.map(renderSingleMenuAsAccordionItem).join('')}
        </div>
    `;
    container.innerHTML = accordionHtml;
}

function showLoading(container) {
    container.innerHTML = '<p class="loading-indicator">Carregando menus...</p>';
}
function showError(container, message) {
    container.innerHTML = `<p class="error-message">Ocorreu um erro ao carregar os menus. Tente novamente mais tarde. <br><small>${message}</small></p>`;
}

export { renderMenu, showLoading, showError };