import { ref } from 'vue'

export async function get(route, jwt) {
    return await fetch(route, {
        method: 'GET',
        headers: {
        'Authorization': `bearer ${jwt}`
        }
    })
}

export async function post(route, body, jwt) {
    return await fetch(route, {
        method: 'POST',
        headers: {
        'Authorization': `bearer ${jwt}`,
        'Content-Type': 'application/json',
        },
        body: JSON.stringify(body)
    })
}

export async function postData(route, data, jwt) {
    return await fetch(route, {
        method: 'POST',
        headers: {
        'Authorization': `bearer ${jwt}`
        },
        body: data
    })
}

export const timeout = ref(false);

export const requestedPage = ref(null);