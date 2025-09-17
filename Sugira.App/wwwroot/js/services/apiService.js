const API_BASE_URL = '/menu/get-all-menus'; // Rota para o seu controller de Menu

/**
 * Busca os menus ativos da API.
 * @returns {Promise<Array>} Uma promessa que resolve para a lista de menus.
 * @throws {Error} Lança um erro se a resposta da API não for bem-sucedida.
 */
async function getMenus() {
    try {
        const response = await fetch(API_BASE_URL, { method: 'GET' });

        if (!response.ok) {
            throw new Error(`Erro na API: ${response.status} ${response.statusText}`);
        }

        return await response.json();
    } catch (error) {
        console.error('Falha ao buscar os menus:', error);
        // Re-lança o erro para que a camada de UI possa tratá-lo.
        throw error;
    }
}

// Exportamos a função para que outros módulos possam usá-la.
export { getMenus };