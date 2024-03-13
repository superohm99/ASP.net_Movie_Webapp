document.getElementById("sendMessageBtn").addEventListener("click", function() {
    // Clear input fields
    document.querySelector("#contactForm textarea").value = "";
    // Submit the form
    document.getElementById("contactForm").submit();
    // redirect to home
    window.alert("Sent successfully!");
  });

  document.getElementById("subject").addEventListener("input", function() {
    var maxLength = 250; // Maximum number of characters
    var currentLength = this.value.length;
    if (currentLength > maxLength) {
      this.value = this.value.substring(0, maxLength);
      currentLength = maxLength;
    }
    document.getElementById("charCount").textContent = currentLength + "/" + maxLength;
  });