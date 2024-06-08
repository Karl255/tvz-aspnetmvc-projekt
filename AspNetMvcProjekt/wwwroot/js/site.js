const API_ROOT = "/api";
const CART_API = API_ROOT + "/cart";
const CART_ITEMS_API = CART_API + "/items"

const JSON_CONENT_TYPE_HEADER = {
	"Content-Type": "application/json",
};

async function addToCart(itemId, refresh) {
	await fetch(CART_ITEMS_API, {
		body: JSON.stringify({ itemId }),
		method: "POST",
		headers: { ...JSON_CONENT_TYPE_HEADER },
	});

	if (refresh) {
		location.reload();
	}
}

async function decreaseInCart(itemId, refresh) {
	await fetch(CART_ITEMS_API, {
		body: JSON.stringify({ itemId }),
		method: "DELETE",
		headers: { ...JSON_CONENT_TYPE_HEADER },
	});

	if (refresh) {
		location.reload();
	}
}
