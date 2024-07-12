<a href="https://www.doji-tech.com/">
  <img src="https://www.doji-tech.com/assets/favicon.ico" alt="doji logo" title="Doji" align="right" height="70" />
</a>

# SentencePiece

[OpenUPM]

A Unity package for [SentencePiece] tokenization.

## About

This package contains the .dll for [Microsoft.ML.Tokenizer] which is part of [ML.NET] as well as the following dependencies:
- Google.Protobuf
- Microsoft.Bcl.AsyncInterfaces
- System.Runtime.CompilerServices.Unsafe
- System.Text.Encodings.Web
- System.Text.Json

---

The main use I have for this is to implement specific tokenizers that rely on SentencePiece (like LLama, T5, ...) as part of the [com.doji.transformers] package.

[OpenUPM]: https://openupm.com/packages/com.doji.sentencepiece
[SentencePiece]: https://github.com/google/sentencepiece
[Microsoft.ML.Tokenizer]: https://github.com/dotnet/machinelearning/tree/main/src/Microsoft.ML.Tokenizers
[ML.NET]: https://dotnet.microsoft.com/en-us/apps/machinelearning-ai/ml-dotnet
[com.doji.transformers]: https://github.com/julienkay/com.doji.transformers
