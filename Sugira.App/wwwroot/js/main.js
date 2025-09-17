// Importamos as funções dos nossos outros módulos.
import { getMenus } from './services/apiService.js';
import { renderMenu, showLoading, showError } from './components/menuRenderer.js';

/**
 * Função principal para inicializar a aplicação.
 */
async function initApp() {
    const menuContainer = document.getElementById('menu-container');

    if (!menuContainer) {
        console.error('Elemento #menu-container não encontrado no DOM.');
        return;
    }

    try {
        // 1. Mostra o estado de carregamento para o usuário.
        showLoading(menuContainer);

        // 2. Chama a API para buscar os dados.
        const menusData = await getMenus();

        // 3. Renderiza os dados recebidos na tela.
        renderMenu(menusData, menuContainer);

    } catch (error) {
        // 4. Se algo der errado, mostra uma mensagem de erro amigável.
        showError(menuContainer, error.message);
    }
}

// Garante que o nosso código só será executado quando a página estiver pronta.
document.addEventListener('DOMContentLoaded', initApp);