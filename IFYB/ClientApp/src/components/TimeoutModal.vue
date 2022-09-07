<template>
  <div class="container" v-if="timeout">
    <div class="row">
      <div class="col-sm-3 col-6 mx-auto">
        <div class="modal fade show" tabindex="-1" aria-labelledby="cookieModal" aria-hidden="true">
          <div class="modal-dialog modal-danger modal-dialog-centered modal-" role="document">
            <div class="modal-content">
              <div class="modal-body p-0">
                <div class="modal-content">
                    <div class="modal-body text-center">
                      <span class="text-primary fs-1 font-weight-bold">
                        <i class="ni ni-time-alarm"></i>
                      </span>
                      <h5 class="modal-title">{{ $t('timeout.title') }}</h5>
                      <p>{{ $t('timeout.subtitle') }}</p>
                    </div>
                    <div class="modal-footer">
                      <button type="button" class="btn bg-gradient-secondary" data-bs-dismiss="modal" @click="close">{{ $t('timeout.later') }}</button>
                      <button type="button" class="btn bg-gradient-primary" data-bs-dismiss="modal" @click="toLogin">{{ $t('timeout.login') }}</button>
                    </div>
                  </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="modal-backdrop fade show" v-if="timeout"></div>
</template>

<script>
import router from '@/router';
import { watch } from 'vue';
import { useTimeout } from "../store";

export default {
  name: "TimeoutModal",
  setup() {
    const { timeout }  =  useTimeout();
    
    function toLogin() {
      timeout.value = false;
      router.push('/authentication');
    }

    function close() {
      timeout.value = false;
    }

    if (timeout.value) {
      router.push('/')
    }

    watch(timeout, () => {
      if (timeout.value) {
        router.push('/')
      }
    });


    return { timeout, toLogin, close };
  },
}
</script>

<style scoped>
.show {
  display: block;
}
</style>