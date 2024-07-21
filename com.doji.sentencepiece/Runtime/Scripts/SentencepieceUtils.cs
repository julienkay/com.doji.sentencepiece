using Microsoft.ML.Tokenizers;
using System;
using System.IO;
using System.Reflection;
using Google.Protobuf;

namespace Doji.AI.Sentencepiece {

    public static class SentencepieceUtils {

        /// <summary>
        /// Loads the sentencepiece model from the given path and overwrites its
        /// "AddDummyPrefix" property
        /// </summary>
        public static SentencePieceBpe LoadModifiedProto(string path, bool addBeginOfSentence, bool addEndOfSentence, bool addDummyPrefix) {
            using FileStream f = new FileStream(path, FileMode.Open, FileAccess.Read);

            var a = Assembly.GetAssembly(typeof(SentencePieceBpe));
            Type modelProtoType = a.GetType("Sentencepiece.ModelProto");

            if (modelProtoType == null) {
               UnityEngine.Debug.LogError("ModelProto type not found.");
                return null;
            }

            // Get the PropertyInfo for the static Parser property
            PropertyInfo parserProperty = modelProtoType.GetProperty("Parser", BindingFlags.Public | BindingFlags.Static);

            if (parserProperty == null) {
                UnityEngine.Debug.LogError("Parser property not found.");
                return null;
            }

            // Get the value of the static Parser property
            object parserInstance = parserProperty.GetValue(null);

            if (parserInstance == null) {
                UnityEngine.Debug.LogError("Failed to get the Parser instance.");
                return null;
            }

            object modifiedProto = (parserInstance as MessageParser).ParseFrom(f);

            if (modifiedProto == null) {
                UnityEngine.Debug.LogError("Failed to parse the model.");
                return null;
            }

            // Get the PropertyInfo for the NormalizerSpec property
            PropertyInfo normalizerSpecProperty = modifiedProto.GetType().GetProperty("NormalizerSpec", BindingFlags.Public | BindingFlags.Instance);

            if (normalizerSpecProperty == null) {
                UnityEngine.Debug.LogError("NormalizerSpec property not found.");
                return null;
            }

            // Get the value of the NormalizerSpec property
            object normalizerSpecInstance = normalizerSpecProperty.GetValue(modifiedProto);

            if (normalizerSpecInstance == null) {
                UnityEngine.Debug.LogError("Failed to get the NormalizerSpec instance.");
                return null;
            }

            // Get the PropertyInfo for the AddDummyPrefix property
            PropertyInfo addDummyPrefixProperty = normalizerSpecInstance.GetType().GetProperty("AddDummyPrefix", BindingFlags.Public | BindingFlags.Instance);

            if (addDummyPrefixProperty == null) {
                UnityEngine.Debug.LogError("AddDummyPrefix property not found.");
                return null;
            }

            // Set the AddDummyPrefix property
            addDummyPrefixProperty.SetValue(normalizerSpecInstance, addDummyPrefix);

            // serialize the modified sentencepiece model and return a new tokenizer created with it
            using MemoryStream modifiedStream = new MemoryStream((modifiedProto as IMessage).ToByteArray());
            return Tokenizer.CreateLlama(modifiedStream, addBeginOfSentence, addEndOfSentence) as SentencePieceBpe;
        }
    }
}
