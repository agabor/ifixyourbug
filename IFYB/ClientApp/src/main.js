import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import { createI18n } from 'vue-i18n'
import { messages } from './utils/i18nMessages'
import VueGtag from "vue-gtag";

const i18n = createI18n({
  locale: 'en',
  messages
});

const app = createApp(App);

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

app.use(router).use(i18n).use(VueGtag, {
  config: { id: "G-TX7L6QHPS3" },
  enabled: JSON.parse(localStorage.getItem('acceptedCookies'))?.analytics ? JSON.parse(localStorage.getItem('acceptedCookies')).analytics : false
}).mount('#app');

function reportError(obj) {
  fetch('api/errors', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(obj)
  })
}

app.config.errorHandler = function (err, vm, info) {
  reportError({ ...err, info, vm })
};

window.onerror = function(event, source, lineno, colno, error) {
  reportError({ ...error, event, lineno, colno, source })
 };