<template>
   <div class="code">
    {{ sshKey }}
   </div>
</template>
<script>
import {useSshKey} from '@/store'
export default {
  name: 'SshKeyPreview',
  setup() {
    return useSshKey()
  }
}
</script>
<style>
.code {
  font-family: monospace;
  font-size: smaller;
  margin-top: 1rem;
  margin-bottom: 1rem;
}
</style>