import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import VueGtag from "vue-gtag";
import { useMessages } from './store/index';
import { fetchPost } from './store/web';

const app = createApp(App);
const { tm } = useMessages();
app.config.globalProperties.$t = tm;
app.config.globalProperties.$filters = {
  dateTimeFormat(value) {
    let options = {
      year: 'numeric', month: 'numeric', day: 'numeric',
      hour: 'numeric', minute: 'numeric'
    };

    const date = new Date(value);
    return new Intl.DateTimeFormat('default', options).format(date);
  },
  dateFormat(value) {
    let options = {
      year: 'numeric', month: 'numeric', day: 'numeric'
    };

    const date = new Date(value);
    return new Intl.DateTimeFormat('default', options).format(date);
  },
  timeFormat(value) {
    let options = {
      hour: 'numeric', minute: 'numeric'
    };

    const date = new Date(value);
    return new Intl.DateTimeFormat('default', options).format(date);
  },
}

let acceptedCookies = JSON.parse(localStorage.getItem('acceptedCookies'));
let visited = localStorage.getItem('visited');
const enabled = acceptedCookies ? acceptedCookies.analytics : false
app.use(router).use(VueGtag, {
  config: { id: "G-TX7L6QHPS3" },
  enabled: visited ? true : enabled,
  bootstrap: true
}, router).mount('#app');

async function reportError(obj) {
  await fetchPost('/api/errors', obj);
}

app.config.errorHandler = async function (err, vm, info) {
  await reportError({ ...err, info, vm })
};

window.onerror = async function(event, source, lineno, colno, error) {
  await reportError({ ...error, event, lineno, colno, source })
};