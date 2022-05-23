<template>
  <div class="page-header min-vh-85">
    <div>
      <img class="position-absolute fixed-top ms-auto w-50 h-100 z-index-0 d-none d-sm-none d-md-block border-radius-lg border-top-end-radius-0 border-top-start-radius-0 border-bottom-end-radius-0" src="../assets/img/bg45.jpg" alt="image">
    </div>
    <div class="container" v-if="page == 'contact'">
      <div class="row">
        <div class="col-lg-7 d-flex justify-content-center flex-column">
          <div class="card shadow-lg d-flex justify-content-center p-4 my-sm-0 my-sm-6 mt-8 mb-5">
            <div class="text-center">
              <h3>Contact us</h3>
              <p class="mb-0">
                For further questions, including partnership opportunities, please email hello@creative-tim.com
                or contact using our contact form.
              </p>
            </div>
            <div class="card-body pb-2">
              <div class="row">
                <div class="col-md-6">
                  <label>Full Name</label>
                  <div class="input-group mb-4">
                    <input id="name-input" class="form-control" placeholder="Full Name" aria-label="Full Name" type="text" v-model="contact.name" autofocus>
                  </div>
                </div>
                <div class="col-md-6 ps-md-2">
                  <label>Email</label>
                  <div class="input-group">
                    <input type="email" id="email-input" class="form-control" placeholder="hello@creative-tim.com" v-model="contact.email">
                  </div>
                </div>
              </div>
              <div class="form-group mb-0 mt-md-0 mt-4">
                <label>How can we help you?</label>
                <textarea name="message" class="form-control border-radius-lg" id="message-input" rows="6" placeholder="Describe your problem in at least 250 characters" v-model="contact.message"></textarea>
              </div>
              <div class="row">
                <div class="alert alert-warning text-white font-weight-bold mt-3 mb-0" role="alert" v-if="error">
                  {{error}}
                </div>
                <div class="col-md-12 text-center mt-3">
                  <button type="submit" class="btn bg-gradient-primary" @click="submitMessage">Send Message</button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <div class="container" v-else-if="page == 'success'">
      <div class="row">
        <div class="col-lg-7 d-flex justify-content-center flex-column">
          <div class="card shadow-lg d-flex justify-content-center p-4 my-sm-0 my-sm-6 mt-8 mb-5">
            <div class="text-center">
              <h3>Thank you!</h3>
              <p class="mb-0">
                We will contact you shortly via email.
              </p>
            </div>
            <div class="card-body pb-2">
              <div class="row">
                <div class="col-md-12 text-center mt-3">
                  <button type="button" class="btn bg-gradient-primary" @click="$router.push('/')">Bact to home</button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { ref } from 'vue';
import { validEmail, required } from '../utils/Validate';

export default {
  name: 'ContactForm',
  setup() {
    const contact = ref({});
    const error = ref(null);
    const page = ref('contact');

    async function submitMessage() {
      let err =
        required(contact.value.name, 'Name', 'name-input') ||
        required(contact.value.email, 'Email', 'email-input') ||
        validEmail(contact.value.email) ||
        required(contact.value.message, 'Message', 'message-input');
      if(err) {
        error.value = err;
      } else {
        try {
          await fetch('/contact', {
            method: 'POST',
            headers: {
              'Content-Type': 'application/json',
            },
            body: JSON.stringify({'name': contact.value.name, 'email': contact.value.email, 'text': contact.value.message})
          });
          page.value = 'success';
          error.value = null;
        } catch {
          error.value = 'Something wrong'
        }
      }
    }
    return { contact, error, page, submitMessage }
  }
}
</script>
