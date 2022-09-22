import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import VueGtag from "vue-gtag";
import { stringify } from 'flatted';
import { useMessages } from './store/index';


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
const enabled = acceptedCookies ? acceptedCookies.analytics : false
app.use(router).use(VueGtag, {
  config: { id: "G-TX7L6QHPS3" },
  enabled: enabled,
  bootstrap: enabled
}, router).mount('#app');

function reportError(obj) {
  fetch('/api/errors', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: stringify(obj)
  })
}

app.config.errorHandler = function (err, vm, info) {
  reportError({ ...err, info, vm })
};

window.onerror = function(event, source, lineno, colno, error) {
  reportError({ ...error, event, lineno, colno, source })
 };