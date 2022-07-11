import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import { createI18n } from 'vue-i18n'
import { messages } from './utils/i18nMessages'

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

app.use(router).use(i18n).mount('#app');
